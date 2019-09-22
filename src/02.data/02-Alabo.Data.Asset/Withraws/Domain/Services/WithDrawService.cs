using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.WithDraw;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.ViewModels.WithDraw;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Extensions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     Class WithDrawService.
    /// </summary>
    public class WithDrawService : ServiceBase, IWithDrawService {

        public WithDrawService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     提现
        ///     申请提现：
        ///     1. 支持允许提现的货币类型，如果货币类型不能提现，则禁止提现
        ///     2. 提现之前对WithdRawConfig提现设置，进行验证
        ///     3. 提现时要冻结资金
        /// </summary>
        /// <param name="view">申请提现</param>
        public ServiceResult Add(WithDrawInput view) {
            var serviceResult = ServiceResult.Success;

            #region 安全验证

            if (view.MoneyTypeId.IsGuidNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("请选择提现账户");
            }

            var user = Resolve<IUserService>().GetUserDetail(view.UserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在");
            }

            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常");
            }

            if (view.PayPassword.ToMd5HashString() != user.Detail.PayPassword) {
                return ServiceResult.FailedWithMessage("支付密码不正确");
            }

            var adminConfig = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
            if (!adminConfig.IsAllowUserWitraw) {
                return ServiceResult.FailedWithMessage("当前时间段系统在进行内部调整,请不要提现。请稍后再试，给您带来的不便深感抱歉!");
            }

            if (view.Amount <= 0) {
                return ServiceResult.FailedWithMessage("提现额度必须大于0");
            }

            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .FirstOrDefault(r => r.Id == view.MoneyTypeId && r.Status == Status.Normal && r.IsWithDraw);
            if (moneyType == null) {
                return ServiceResult.FailedWithMessage("当前账户不支持提现"); //或状态不正常。请在控制面板中进入设置
            }

            var withDrawConfig = Resolve<IAutoConfigService>().GetValue<WithdRawConfig>();
            if (withDrawConfig == null) {
                return ServiceResult.FailedWithMessage("提现配置异常");
            }

            if (!withDrawConfig.CanWithdRaw) {
                return ServiceResult.FailedWithMessage(withDrawConfig.CanNotWithRawIntro);
            }

            var userAcount = Resolve<IAccountService>()
                .GetSingle(e => e.MoneyTypeId == view.MoneyTypeId && e.UserId == view.UserId);
            if (userAcount.Amount < view.Amount) {
                return ServiceResult.FailedWithMessage("提现额度不足");
            }

            //if (withDrawConfig.IsIdentity) {
            //    if (user.Detail.IdentityStatus != IdentityStatus.IsChecked) {
            //        return ServiceResult.FailedWithMessage($"提现前需实名认证");
            //    }
            //}

            if (withDrawConfig.MinAmount > view.Amount) {
                return ServiceResult.FailedWithMessage($"提现额度不能小于{withDrawConfig.MinAmount}");
            }

            if (withDrawConfig.MaxAmount < view.Amount) {
                return ServiceResult.FailedWithMessage($"提现额度不能大于{withDrawConfig.MaxAmount}");
            }

            if (withDrawConfig.IsMinAmountMultiple != 0 && withDrawConfig.MinAmount != 0 &&
                view.Amount % withDrawConfig.MinAmount != 0) {
                return ServiceResult.FailedWithMessage($"提现额度必须为{withDrawConfig.MinAmount}的倍数");
            }

            if (!withDrawConfig.CanSerialWithDraw) {
                if (Resolve<ITradeService>().GetList(e =>
                        e.Type == TradeType.Withraw && e.Status != TradeStatus.Failured &&
                        e.Status != TradeStatus.Success).Count() > 0) {
                    return ServiceResult.FailedWithMessage("您有一笔提现正在处理中，本次提现已被取消！");
                }
            }

            #endregion 安全验证

            var trade = AutoMapping.SetValue<Trade>(view);
            trade.Type = TradeType.Withraw;
            //银行卡信息处理
            var backCard = Resolve<IBankCardService>().GetSingle(u => u.Number == view.BankCardId);
            //var backCard = new BankCard();
            trade.TradeExtension.TradeRemark = new TradeRemark { UserRemark = view.UserRemark };
            trade.TradeExtension.WithDraw = new TradeWithDraw {
                BankCard = backCard,
                CheckAmount = view.Amount * (1 - withDrawConfig.Fee),
                Fee = view.Amount * withDrawConfig.Fee
            };

            var userBill = Resolve<IBillService>().CreateBill(userAcount, -view.Amount, BillActionType.Treeze);
            userBill.Intro = $"用户{user.GetUserName()}发起提现操作,{moneyType.Name}冻结{view.Amount}";

            userAcount.HistoryAmount = userAcount.Amount;
            userAcount.Amount -= view.Amount;
            userAcount.FreezeAmount += view.Amount;

            trade.TradeExtension.WithDraw.AfterAmount = userBill.AfterAmount;
            trade.Extension = trade.TradeExtension.ToJson();

            var context = Repository<ITradeRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<ITradeService>().Add(trade);
                Resolve<IAccountService>().Update(userAcount);
                userBill.EntityId = trade.Id;
                Resolve<IBillService>().Add(userBill);
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return serviceResult;
        }

        /// <summary>
        ///     用户删除提现，后取消提现，审核成功的提现记录不能删除
        ///     删除提现：只能对未审核的提现进行删除
        ///     删除提现时：冻结的资金需要解冻
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        public ServiceResult Delete(long userId, long id) {
            var serviceResult = ServiceResult.Success;
            var trade = Resolve<ITradeService>()
                .GetSingle(e => e.Type == TradeType.Withraw && e.UserId == userId && e.Id == id);
            var user = Resolve<IUserService>().GetSingle(userId);
            var userAcount = Resolve<IAccountService>()
                .GetSingle(e => e.UserId == userId && e.MoneyTypeId == trade.MoneyTypeId);
            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .FirstOrDefault(r => r.Id == trade.MoneyTypeId && r.Status == Status.Normal);
            if (trade != null) {
                if (trade.Status == TradeStatus.Pending) {
                    var userBill = Resolve<IBillService>()
                        .CreateBill(userAcount, trade.Amount, BillActionType.DeductTreeze);
                    userBill.Intro = $"用户{user.GetUserName()}撤销提现操作,{moneyType.Name}解冻{trade.Amount}";
                    userBill.EntityId = trade.Id;

                    userAcount.HistoryAmount = userAcount.Amount;
                    userAcount.Amount += trade.Amount;
                    userAcount.FreezeAmount -= trade.Amount;

                    var context = Repository<ITradeRepository>().RepositoryContext;
                    context.BeginTransaction();
                    try {
                        Resolve<ITradeService>().Delete(e => e.Id == trade.Id);
                        Resolve<IBillService>().Add(userBill);
                        context.SaveChanges();
                        context.CommitTransaction();
                    } catch (Exception ex) {
                        context.RollbackTransaction();
                        return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
                    } finally {
                        context.DisposeTransaction();
                    }
                }

                if (trade.Status == TradeStatus.Failured) {
                    Resolve<ITradeService>().Delete(e => e.Id == trade.Id);
                    return serviceResult;
                }

                if (trade.Status == TradeStatus.Success) {
                    return ServiceResult.FailedWithMessage("提现成功记录不允许删除!");
                }

                return ServiceResult.FailedWithMessage("管理审核提现记录不允许删除!");
            }

            return ServiceResult.FailedWithMessage("当前提现记录不存在!");
        }

        /// <summary>
        ///     获取提现详情
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        public WithDrawShowOutput GetSingle(long userId, long id) {
            var trade = Resolve<ITradeService>()
                .GetSingle(r => r.Id == id && r.UserId == userId && r.Type == TradeType.Withraw);
            var monenyTypeConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            if (trade != null) {
                var user = Resolve<IUserService>().GetSingle(userId);
                var moneyConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
                var withDrawOutput = AutoMapping.SetValue<WithDrawShowOutput>(trade);
                withDrawOutput.Fee = trade.TradeExtension.WithDraw.Fee;
                withDrawOutput.UserName = user.UserName;
                withDrawOutput.PayTime = trade.PayTime.ToString("yyyy-MM-dd HH:mm:ss");
                withDrawOutput.CreateTime = trade.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                withDrawOutput.Status = trade.Status.GetDisplayName();
                withDrawOutput.MoneyTypeConfig = monenyTypeConfigs.SingleOrDefault(e => e.Id == trade.MoneyTypeId);
                withDrawOutput.Remark = trade.TradeExtension.TradeRemark.Remark;
                withDrawOutput.UserRemark = trade.TradeExtension.TradeRemark.UserRemark;
                withDrawOutput.BankCardInfo =
                    $"持卡人:[{trade.TradeExtension.WithDraw.BankCard.Name}]卡号:[{trade.TradeExtension.WithDraw.BankCard.Number}]{trade.TradeExtension.WithDraw.BankCard.Type.GetDisplayName()}";
                return withDrawOutput;
            }

            return null;
        }

        public PagedList<Trade> GetUserWithDrawList(WithDrawApiInput parameter) {
            var query = new ExpressionQuery<Trade> {
                PageIndex = (int)parameter.PageIndex,
                PageSize = (int)parameter.PageSize
            };
            query.And(e => e.UserId == parameter.LoginUserId);
            query.And(e => e.Type == TradeType.Withraw);
            query.OrderByDescending(r => r.Id);
            var model = Resolve<ITradeService>().GetPagedList(query);
            return model;
        }

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<WithDrawOutput> GetPageList(object query) {
            // 获取当前供应商的所有订单
            var list = Resolve<ITradeService>().GetPagedList(query, r => r.Type == TradeType.Withraw);
            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var users = Resolve<IUserService>().GetList();
            var resultList = new List<WithDrawOutput>();
            var model = new WithDrawOutput();
            foreach (var item in list) {
                model = AutoMapping.SetValue<WithDrawOutput>(item);
                if (item.TradeExtension != null) {
                    model.Fee = item.TradeExtension.WithDraw.Fee;
                    model.UserRemark = item.TradeExtension.TradeRemark.UserRemark;
                }

                model.MoneyTypeName = moneyType.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name;
                model.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
                model.BankName = item.TradeExtension?.WithDraw?.BankCard?.Address;
                model.CardId = item.TradeExtension?.WithDraw?.BankCard?.Number;
                model.RealName = item.TradeExtension?.WithDraw?.BankCard?.Name;

                resultList.Add(model);
            }

            return PagedList<WithDrawOutput>.Create(resultList, list.Count, list.PageSize, list.PageIndex);
        }

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<ViewAdminWithDraw> GetAdminPageList(object query) {
            // 获取当前供应商的所有订单
            var list = Resolve<ITradeService>().GetPagedList(query, r => r.Type == TradeType.Withraw);
            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var users = Resolve<IUserService>().GetList();
            var resultList = new List<ViewAdminWithDraw>();
            var model = new ViewAdminWithDraw();
            foreach (var item in list) {
                model = AutoMapping.SetValue<ViewAdminWithDraw>(item);
                if (item.TradeExtension != null) {
                    model.Fee = item.TradeExtension.WithDraw.Fee;
                    model.UserRemark = item?.TradeExtension?.TradeRemark?.UserRemark;
                }

                model.MoneyTypeConfig = moneyType.FirstOrDefault(u => u.Id == item.MoneyTypeId);
                model.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
                model.BankCard = item.TradeExtension?.WithDraw?.BankCard;
                model.StatusName = item.Status.GetDisplayName();

                resultList.Add(model);
            }

            return PagedList<ViewAdminWithDraw>.Create(resultList, list.Count, list.PageSize, list.PageIndex);
        }

        /// <summary>
        ///     提现详情
        /// </summary>
        /// <param name="id">Id标识</param>
        public ViewAdminWithDraw GetAdminWithDraw(long id) {
            var withDraw = Resolve<ITradeService>().GetSingle(e => e.Id == id);
            if (withDraw == null) {
                return new ViewAdminWithDraw();
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var model = AutoMapping.SetValue<ViewAdminWithDraw>(withDraw);
            model.User = Resolve<IUserService>().GetSingle(withDraw.UserId);
            var grades = Resolve<IGradeService>().GetUserGradeList();
            if (withDraw.TradeExtension != null) {
                model.Fee = withDraw.TradeExtension.WithDraw.Fee;
                model.UserRemark = withDraw.TradeExtension.TradeRemark.UserRemark;
                model.Remark = withDraw.TradeExtension.TradeRemark.Remark;
                model.FailuredReason = withDraw.TradeExtension.TradeRemark.FailuredReason;
            }

            model.AfterAmount = withDraw.TradeExtension.WithDraw.AfterAmount;

            //应付人民币
            model.CheckAmount = withDraw.TradeExtension.WithDraw.CheckAmount;
            model.ActualAmount = withDraw.Amount;
            model.UserGrade = grades.FirstOrDefault(r => r.Id == model.User?.GradeId);
            model.BankCard = withDraw.TradeExtension.WithDraw.BankCard;
            model.MoneyTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == withDraw.MoneyTypeId);
            //下一条账单
            model.NextWithDraw = Resolve<ITradeService>().Next(withDraw);

            //上一条账单

            model.PrexWithDraw = Resolve<ITradeService>().Prex(withDraw);
            if (model.PrexWithDraw == null) {
                model.PrexWithDraw = withDraw;
            }

            return model;
        }

        /// <summary>
        ///     获取所有支持提现的货币类型
        /// </summary>
        public IList<KeyValue> GetWithDrawMoneys(long userId) {
            var moneys = Resolve<IAutoConfigService>().MoneyTypes();
            var userAcounts = Resolve<IAccountService>().GetUserAllAccount(userId);
            var dic = new Dictionary<string, object>();
            moneys.Foreach(r => {
                if (r.IsWithDraw) {
                    var account = userAcounts.FirstOrDefault(e => e.MoneyTypeId == r.Id);
                    dic.Add(r.Id.ToString(), r.Name + $"[余额:{account.Amount}]");
                }
            });

            var list = new List<KeyValue>();
            foreach (var item in dic) {
                var keyValue = new KeyValue {
                    Value = item.Value,
                    Key = item.Key
                };
                list.Add(keyValue);
            }

            return list;
        }

        /// <summary>
        ///     提现审核  包含初审和终审
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public ServiceResult Check(DefaultHttpContext httpContext) {
            var view = AutoMapping.SetValue<ViewWithDrawCheck>(httpContext);

            return WithDrawCheck(view);
        }

        /// <summary>
        ///     获取用户提现列表
        /// </summary>
        /// <param name="parameter">参数</param>
        public PagedList<Trade> GetUserList(WithDrawApiInput parameter) {
            var query = new ExpressionQuery<Trade> {
                PageIndex = (int)parameter.PageIndex,
                PageSize = (int)parameter.PageSize
            };
            query.And(e => e.UserId == parameter.LoginUserId);
            query.And(e => e.Type == TradeType.Withraw);
            query.OrderByDescending(r => r.Id);
            var model = Resolve<ITradeService>().GetPagedList(query);
            return model;
        }

        /// <summary>
        ///     提现审核
        /// </summary>
        /// <param name="view">The 视图.</param>
        public ServiceResult WithDrawCheck(ViewWithDrawCheck view) {
            var result = ServiceResult.Success;

            #region 数据校验

            if (view.Status == TradeStatus.Failured) {
                if (view.FailuredReason.IsNullOrEmpty()) {
                    return ServiceResult.FailedWithMessage("审核失败，失败原因不能为空");
                }
            }

            var withDraw = Resolve<ITradeService>().GetSingle(r => r.Id == view.TradeId);
            if (withDraw == null) {
                return ServiceResult.FailedWithMessage("用户没有提现记录");
            }

            var user = Resolve<IUserService>().GetSingle(withDraw.UserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("提现用户不存在");
            }

            var moneyTypeList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal);
            if (moneyTypeList == null || moneyTypeList.Count == 0) {
                return ServiceResult.FailedWithMessage("货币配置不存在或状态不正常，请在控制面板中进入设置");
            }

            var drawMoneyType = moneyTypeList.FirstOrDefault(r =>
                r.Id == withDraw.MoneyTypeId && r.Status == Status.Normal && r.IsWithDraw);
            if (drawMoneyType == null) {
                return ServiceResult.FailedWithMessage("提现账户货币类型不存在，或状态不正常。请在控制面板中进入设置");
            }

            var account = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == drawMoneyType.Id);
            if (account == null) {
                return ServiceResult.FailedWithMessage("用户无该提现账户");
            }

            #endregion 数据校验

            var context = Repository<ITradeRepository>().RepositoryContext;
            if (view.Status == TradeStatus.Failured) {
                //数据准备
                var bill = new Bill {
                    EntityId = withDraw.Id,
                    Intro =
                        $@"{user.GetUserName()}申请{drawMoneyType.Name}提现{
                                withDraw.Amount
                            }，管理员初审不通过，账户退回{withDraw.Amount}",
                    UserId = withDraw.UserId,
                    Type = BillActionType.Withraw,
                    Flow = AccountFlow.Income,
                    MoneyTypeId = withDraw.MoneyTypeId,
                    Amount = withDraw.Amount,
                    AfterAmount = account.Amount + withDraw.Amount
                };
                if (withDraw.Status == TradeStatus.FirstCheckSuccess) {
                    bill.Intro =
                        $@"{user.GetUserName()}申请{drawMoneyType.Name}提现{
                                withDraw.Amount
                            }，管理员二审不通过，账户退回{withDraw.Amount}";
                }

                withDraw.Status = view.Status;
                withDraw.TradeExtension.TradeRemark.Remark =
                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";
                withDraw.TradeExtension.TradeRemark.FailuredReason = view.FailuredReason;
                withDraw.Extension = withDraw.TradeExtension.ToJson();
                account.Amount += withDraw.Amount;
                account.FreezeAmount -= withDraw.Amount;
                context.BeginTransaction();

                try {
                    Resolve<IAccountService>().Update(account);
                    Resolve<ITradeService>().Update(withDraw);
                    Resolve<IBillService>().Add(bill);
                    context.SaveChanges();
                    context.CommitTransaction();
                } catch (Exception ex) {
                    context.RollbackTransaction();
                    return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
                } finally {
                    context.DisposeTransaction();
                }
            }

            // 审核成功
            if (view.Status == TradeStatus.Success) {
                var bill = new Bill {
                    EntityId = withDraw.Id,
                    Intro = $@"{user.GetUserName()}申请{drawMoneyType.Name}提现{withDraw.Amount}成功",
                    UserId = withDraw.UserId,
                    Type = BillActionType.Withraw,
                    Flow = AccountFlow.Spending,
                    MoneyTypeId = withDraw.MoneyTypeId,
                    Amount = withDraw.Amount,
                    AfterAmount = account.Amount
                };
                withDraw.PayTime = DateTime.Now;
                withDraw.Status = view.Status;
                withDraw.TradeExtension.TradeRemark.Remark =
                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";

                withDraw.TradeExtension.TradeRemark.Remark = view.Remark;
                withDraw.Extension = withDraw.TradeExtension.ToJson();

                account.FreezeAmount -= withDraw.Amount;

                //TODO 9月重构注释
                //var shareOrder = new ShareOrder {
                //    UserId = withDraw.UserId,
                //    Amount = withDraw.Amount,
                //    Status = ShareOrderStatus.Pending,
                //    TriggerType = TriggerType.WithDraw,
                //    EntityId = withDraw.Id
                //};

                context.BeginTransaction();
                try {
                    Resolve<IAccountService>().Update(account);
                    Resolve<ITradeService>().Update(withDraw);
                    Resolve<IBillService>().Add(bill);
                    //TODO 9月重构注释
                    // Resolve<IShareOrderService>().Add(shareOrder);
                    context.SaveChanges();
                    context.CommitTransaction();
                } catch (Exception ex) {
                    context.RollbackTransaction();
                    return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
                } finally {
                    context.DisposeTransaction();
                }
            }

            if (view.Status == TradeStatus.FirstCheckSuccess) {
                withDraw.Status = view.Status;
                withDraw.TradeExtension.TradeRemark.Remark =
                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";
                withDraw.TradeExtension.TradeRemark.Remark = view.Remark;
                withDraw.Extension = withDraw.TradeExtension.ToJson();
                Resolve<ITradeService>().Update(withDraw);
            }

            return result;
        }

        /// <summary>
        ///     会员中心提现记录
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ViewHomeWithDraw> GetUserPage(object query) {
            var user = query.ToUserObject();
            var resultList = Resolve<ITradeService>()
                .GetPagedList<ViewHomeWithDraw>(query, r => r.Type == TradeType.Withraw && r.UserId == user.Id);
            var moenyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            resultList.ForEach(r => {
                r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name;
                if (r.TradeExtension != null) {
                    r.Fee = r.TradeExtension.WithDraw.Fee;
                }
            });
            return resultList;
        }

        /// <summary>
        ///     提现Edit
        /// </summary>
        public ViewHomeWithDraw GetView(long id) {
            var viewHomeWithDraw = new ViewHomeWithDraw();
            return viewHomeWithDraw;
        }
    }
}