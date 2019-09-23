using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Asset.Recharges.Domain.Entities;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Recharge;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.ViewModels.Recharge;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     Class RechargeService.
    /// </summary>
    public class RechargeService : ServiceBase<Recharge, long>, IRechargeService {

        public RechargeService(IUnitOfWork unitOfWork, IRepository<Recharge, long> repository) : base(unitOfWork, repository) {
        }

        public ServiceResult StoredValueRecharge(StoredValueInput view) {
            var user = Resolve<IUserService>().GetSingleByUserNameOrMobile(view.UserName);
            var userAccount = Resolve<IAccountService>().GetSingle(u => u.UserId == user.Id && u.MoneyTypeId == Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000"));
            var context = Repository<ITradeRepository>().RepositoryContext;

            var trade = new Trade {
                Status = TradeStatus.Pending,
                MoneyTypeId = Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000"),
                UserId = user.Id,
                Type = TradeType.Recharge
            };
            trade.TradeExtension.Recharge.CheckAmount = view.Amount;
            trade.Extension = trade.TradeExtension.ToJson();

            try {
                //开启事务
                context.BeginTransaction();
                trade.Amount = view.Amount;
                var result = Resolve<ITradeService>().Add(trade);
                if (!result) {
                    //插入数据失败 事物回滚
                    context.RollbackTransaction();
                }
                userAccount.Amount += view.Amount;
                result = Resolve<IAccountService>().Update(userAccount);
                if (!result) {
                    //充值失败 事物回滚
                    context.RollbackTransaction();
                }
                var bill = new Bill {
                    Intro = $@"{view.UserName}充值{view.Amount}成功",
                    UserId = user.Id,
                    Type = BillActionType.Recharge,
                    Flow = AccountFlow.Income,
                    MoneyTypeId = trade.MoneyTypeId,
                    Amount = view.Amount,
                    AfterAmount = userAccount.Amount
                };
                Resolve<IBillService>().Add(bill);
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception e) {
                //发生错误 事物回滚
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage(e.ToString());
            } finally {
                context.DisposeTransaction();
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     用户删除充值，后取消充值，审核成功的充值记录不能删除
        /// </summary>
        /// <param name="userId">会员Id</param>
        /// <param name="id">Id标识</param>
        /// <exception cref="NotImplementedException"></exception>
        public ServiceResult Delete(long userId, long id) {
            var trade = Resolve<ITradeService>().GetSingle(r => r.Id == id);
            if (trade == null) {
                return ServiceResult.FailedWithMessage("记录不存在");
            }

            if (trade.UserId != userId) {
                return ServiceResult.FailedWithMessage("您无权删除他人记录");
            }

            throw new NotImplementedException();
        }

        /// <summary>
        ///     审核  包含初审和终审
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public ServiceResult Check(DefaultHttpContext httpContext) {
            var view = AutoMapping.SetValue<ViewRechargeCheck>(httpContext);
            return RechargeCheck(view);
        }

        /// <summary>
        ///     获取所有支持充值的货币类型
        /// </summary>
        public IList<KeyValue> GetRechargeMoneys() {
            var moneys = Resolve<IAutoConfigService>().MoneyTypes();
            var dic = new Dictionary<string, object>();
            moneys.Foreach(r => {
                if (r.IsRecharge) {
                    dic.Add(r.Id.ToString(), r.Name);
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
        ///     获取充值详情
        /// </summary>
        /// <param name="id">Id标识</param>
        public RechargeDetail GetSingle(long id) {
            var trade = Resolve<ITradeService>().GetSingle(e => e.Id == id && e.Type == TradeType.Recharge);

            if (trade is null) {
                return null;
            }

            var MonyTypeConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            return new RechargeDetail {
                Id = trade.Id,
                UserId = trade.UserId,
                Serial = trade.Serial,
                Account = trade.Amount,
                Fee = trade.TradeExtension.Recharge.Fee,
                DateTime = trade.CreateTime.ToString("yyyy-MM-dd hh:ss"),
                moneyTypeName =
                    $"{MonyTypeConfigs.SingleOrDefault(e => e.Id == trade.MoneyTypeId).Name}[{trade.TradeExtension.Recharge.CheckAmount}]",
                ChargeType = trade.TradeExtension.Recharge.RechargeType.GetDisplayName(),
                Status = trade.Status.GetDisplayName(),
                Message = trade.TradeExtension.TradeRemark.UserRemark,
                Description = trade.TradeExtension.Recharge.RechargeType == RechargeType.Online
                    ? $"{trade.TradeExtension.Recharge.PayType.GetDisplayName()}"
                    : $"银行卡支付，持卡人[{trade.TradeExtension.Recharge.BankName}]卡号:[{trade.TradeExtension.Recharge.BankNumber}]{trade.TradeExtension.Recharge.BankType.GetDisplayName()}"
            };
        }

        /// <summary>
        ///     获取用户充值列表
        /// </summary>
        /// <param name="parameter"></param>
        public PagedList<Trade> GetUserList(RechargeAddInput parameter) {
            var query = new ExpressionQuery<Trade> {
                PageIndex = (int)parameter.PageIndex,
                PageSize = (int)parameter.PageSize
            };
            if (parameter.LoginUserId > 0) {
                query.And(e => e.UserId == parameter.LoginUserId);
            }

            query.And(e => e.Type == TradeType.Recharge);
            query.OrderByDescending(r => r.CreateTime);
            var model = Resolve<ITradeService>().GetPagedList(query);
            return model;
        }

        /// <summary>
        ///     Recharges the check.
        /// </summary>
        /// <param name="view">The view.</param>
        public ServiceResult RechargeCheck(ViewRechargeCheck view) {
            var result = ServiceResult.Success;
            var trade = Resolve<ITradeService>().GetSingle(e => e.Id == view.Id);
            var user = Resolve<IUserService>().GetSingle(trade.UserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("充值用户不存在");
            }

            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().FirstOrDefault(r =>
                r.Id == trade.MoneyTypeId && r.Status == Status.Normal && r.IsRecharge);
            if (moneyType == null) {
                return ServiceResult.FailedWithMessage("充值账户不存在，或状态不正常。请在控制面板中进入设置");
            }

            var rechargeConfig = Resolve<IAutoConfigService>().GetValue<RechargeConfig>();
            if (rechargeConfig == null) {
                return ServiceResult.FailedWithMessage("充值配置不存在");
            }

            var account = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == moneyType.Id);
            if (account == null) {
                return ServiceResult.FailedWithMessage("用户无该充值账户");
            }

            var context = Repository<ITradeRepository>().RepositoryContext;
            //  充值审核  充值一审
            if (view.Status == TradeStatus.Failured) {
                trade.Status = view.Status;
                trade.TradeExtension.TradeRemark.Remark = $"{trade.TradeExtension.TradeRemark.Remark} {view.Remark}";
                trade.TradeExtension.TradeRemark.FailuredReason = view.FailuredReason;
                trade.Extension = trade.TradeExtension.ToJson();
                Resolve<ITradeService>().Update(trade);
                return result;
            }

            if (view.Status == TradeStatus.FirstCheckSuccess) {
                trade.Status = view.Status;
                trade.TradeExtension.TradeRemark.Remark = $"{trade.TradeExtension.TradeRemark.Remark} {view.Remark}";
                trade.TradeExtension.TradeRemark.FailuredReason = view.FailuredReason;
                trade.Extension = trade.TradeExtension.ToJson();
                Resolve<ITradeService>().Update(trade);
                return result;
            }

            if (view.Status == TradeStatus.Success) {
                if (trade.Status == TradeStatus.Pending) {
                    trade.Status = view.Status;
                    trade.TradeExtension.TradeRemark.Remark =
                        $"{trade.TradeExtension.TradeRemark.Remark} {view.Remark}";
                    trade.Extension = trade.TradeExtension.ToJson();
                    Resolve<ITradeService>().Update(trade);
                }

                if (trade.Status == TradeStatus.FirstCheckSuccess) {
                    var bill = new Bill {
                        EntityId = trade.Id,
                        Intro = $@"{user.GetUserName()}申请{moneyType.Name}充值{trade.Amount}成功",
                        UserId = trade.UserId,
                        Type = BillActionType.Recharge,
                        Flow = AccountFlow.Income,
                        MoneyTypeId = trade.MoneyTypeId,
                        Amount = trade.Amount,
                        AfterAmount = account.Amount + trade.Amount
                    };
                    trade.PayTime = DateTime.Now;
                    trade.Status = view.Status;
                    trade.TradeExtension.TradeRemark.Remark =
                        $"{trade.TradeExtension.TradeRemark.Remark} {view.Remark}";
                    trade.Extension = trade.TradeExtension.ToJson();
                    account.Amount += trade.Amount;
                    //TODO 9月重构注释
                    // 插入 充值人民币
                    //var shareOrder = new ShareOrder {
                    //    UserId = trade.UserId,
                    //    Amount = trade.TradeExtension.Recharge.CheckAmount,
                    //    Status = ShareOrderStatus.Pending,
                    //    TriggerType = TriggerType.Recharge,
                    //    EntityId = trade.Id
                    //};

                    context.BeginTransaction();
                    try {
                        Resolve<IAccountService>().Update(account);
                        Resolve<ITradeService>().Update(trade);
                        Resolve<IBillService>().Add(bill);
                        //  Resolve<IShareOrderService>().Add(shareOrder);
                        context.SaveChanges();
                        context.CommitTransaction();
                    } catch (Exception ex) {
                        context.RollbackTransaction();
                        return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
                    } finally {
                        context.DisposeTransaction();
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the user page.
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<ViewHomeRecharge> GetUserPage(object query) {
            var user = query.ToUserObject();
            var model = Resolve<ITradeService>()
                .GetPagedList<ViewHomeRecharge>(query, r => r.Type == TradeType.Recharge && r.UserId == user.Id);
            return model;
        }

        /// <summary>
        ///     线下充值Edit
        /// </summary>
        public ViewHomeRecharge GetView(long id) {
            var viewHomeRecharge = new ViewHomeRecharge();
            return viewHomeRecharge;
        }

        #region 两种充值方式

        /// <summary>
        ///     线上充值
        /// </summary>
        /// <param name="view"></param>
        public Tuple<ServiceResult, Pay> AddOnline(RechargeOnlineAddInput view) {
            var user = Resolve<IUserService>().GetSingle(view.LoginUserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户异常"), new Pay());
            }

            var RechargeConfig = Resolve<IAutoConfigService>().GetValue<RechargeConfig>();
            if (RechargeConfig == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("充值配置不存在，请在控制面板中添加充值配置"), new Pay()); //
            }

            if (RechargeConfig.Status != Status.Normal) {
                return Tuple.Create(ServiceResult.FailedWithMessage("充值配置状态不正常，请在控制面板中设置"), new Pay());
            }

            //if (!RechargeConfig.Openrecharge) {
            //    return Tuple.Create(ServiceResult.FailedWithMessage("充值配置设置为不允许充值，请在控制面板中设置"), new Pay());
            //}

            if (view.Amount < RechargeConfig.RechargeAmountMin) {
                return Tuple.Create(ServiceResult.FailedWithMessage("充值额度不能小于充值配置里的最小金额"), new Pay());
            }

            if (view.Amount <= 0) {
                return Tuple.Create(ServiceResult.FailedWithMessage("充值额度不能为0"), new Pay());
            }

            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .FirstOrDefault(r => r.Id == view.MoneyTypeId && r.Status == Status.Normal);
            if (moneyType == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("充值货币类型不存在或状态异常"), new Pay());
            }

            var userAcount = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == moneyType.Id);
            if (userAcount == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户无该充值账户"), new Pay());
            }

            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();

            var trade = new Trade {
                Status = TradeStatus.Pending,
                Amount = view.Amount * (1 - RechargeConfig.ServiceRate) / moneyType.RateFee, //到账金额
                MoneyTypeId = moneyType.Id,
                UserId = user.Id,
                Type = TradeType.Recharge
            };

            trade.TradeExtension.Recharge = new RechargeExtension { RechargeType = RechargeType.Online };
            trade.TradeExtension.Recharge.CheckAmount = view.Amount; //实际支付人民币
            trade.TradeExtension.Recharge.Fee = view.Amount * RechargeConfig.ServiceRate; //手续费

            trade.TradeExtension.TradeRemark.UserRemark = view.Remark;

            trade.Extension = trade.TradeExtension.ToJson();

            var pay = new Pay {
                Status = PayStatus.WaiPay,
                Type = CheckoutType.Recharge,
                Amount = trade.Amount,
                UserId = user.Id
            };
            var payExtension = new PayExtension {
                TradeNo = trade.Serial,
                Body = $"您正在{webSite.WebSiteName}商城上充值，请认真核对账单信息",
                UserName = user.GetUserName()
            };

            var context = Repository<ITradeRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                Resolve<ITradeService>().Add(trade);
                pay.EntityId = $"[{trade.Id}]";
                payExtension.TradeNo = trade.Serial;
                pay.Extensions = payExtension.ToJson();
                Resolve<IPayService>().Add(pay);
                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();

                return Tuple.Create(ServiceResult.FailedWithMessage("更新失败:" + ex.Message), new Pay());
            } finally {
                context.DisposeTransaction();
            }

            return Tuple.Create(ServiceResult.Success, pay);
        }

        /// <summary>
        ///     线下充值
        /// </summary>
        /// <param name="view"></param>
        public ServiceResult AddOffOnline(RechargeAddInput view) {
            var rechargeConfig = Resolve<IAutoConfigService>().GetValue<RechargeConfig>();
            var user = Resolve<IUserService>().GetSingle(view.LoginUserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("当前用户不存在");
            }

            if (rechargeConfig == null) {
                return ServiceResult.FailedWithMessage("充值配置不存在，请在控制面板中添加充值配置");
            }

            if (rechargeConfig.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("充值配置状态不正常，请在控制面板中设置");
            }

            if (!rechargeConfig.Openrecharge) {
                return ServiceResult.FailedWithMessage("充值配置设置为不允许充值，请在控制面板中设置");
            }

            if (view.Amount < rechargeConfig.RechargeAmountMin) {
                return ServiceResult.FailedWithMessage("充值额度不能小于充值配置里的最小金额");
            }

            if (view.Amount <= 0) {
                return ServiceResult.FailedWithMessage("充值额度不能为0");
            }

            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .FirstOrDefault(r => r.Id == view.MoneyTypeId && r.Status == Status.Normal);
            if (moneyType == null) {
                return ServiceResult.FailedWithMessage("充值货币类型不存在或状态异常");
            }

            var userAcount = Resolve<IAccountService>()
                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == moneyType.Id);
            if (userAcount == null) {
                return ServiceResult.FailedWithMessage("用户无该充值账户");
            }

            var trade = new Trade {
                Status = TradeStatus.Pending,
                Amount = view.Amount * (1 - rechargeConfig.ServiceRate) / moneyType.RateFee, //到账金额
                MoneyTypeId = moneyType.Id,
                UserId = user.Id,
                Type = TradeType.Recharge
            };

            trade.TradeExtension.Recharge.CheckAmount = view.Amount; //实际支付人民币
            trade.TradeExtension.Recharge.BankNumber = view.BankNumber;
            trade.TradeExtension.Recharge.BankType = view.BankType;
            trade.TradeExtension.Recharge.BankName = view.BankName;
            trade.TradeExtension.Recharge.Fee = view.Amount * rechargeConfig.ServiceRate; //手续费
            trade.TradeExtension.Recharge.RechargeType = RechargeType.Offline;
            trade.TradeExtension.TradeRemark.UserRemark = view.Remark;
            trade.Extension = trade.TradeExtension.ToJson();
            Resolve<ITradeService>().Add(trade);
            return ServiceResult.Success;
        }

        #endregion 两种充值方式

        #region 后台分页及详情

        /// <summary>
        ///     后台分页
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<RechargeOutput> GetPageList(object query) {
            var resultList = Resolve<ITradeService>()
                .GetPagedList<RechargeOutput>(query, r => r.Type == TradeType.Recharge);
            var moenyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            resultList.ForEach(r => {
                r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name;
                r.StatusName = r.Status.GetDisplayName();
                r.RechargeType = r.Extension.DeserializeJson<TradeExtension>().Recharge.RechargeType;
            });

            return resultList;
        }

        /// <summary>
        ///     充值详情
        /// </summary>
        /// <param name="id">Id标识</param>
        public ViewAdminRecharge GetAdminRecharge(long id) {
            var recharge = Resolve<ITradeService>().GetSingle(e => e.Id == id && e.Type == TradeType.Recharge);
            if (recharge == null) {
                return null;
            }

            var model = new ViewAdminRecharge();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            model.User = Resolve<IUserService>().GetSingle(recharge.UserId);
            model.Trade = recharge;
            var grades = Resolve<IGradeService>().GetUserGradeList();
            model.UserGrade = grades.FirstOrDefault(r => r.Id == model.User?.GradeId);
            model.MoneyTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == recharge.MoneyTypeId);
            model.Trade.TradeExtension = model.Trade.Extension.DeserializeJson<TradeExtension>();

            model.NextRecharge = Resolve<ITradeService>().Next(recharge);
            model.PrexRecharge = Resolve<ITradeService>().Prex(recharge);

            return model;
        }

        #endregion 后台分页及详情
    }
}