//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Alabo.App.Asset.Transfers.Domain.Entities;
//using Alabo.App.Core.Common.Domain.CallBacks;
//using Alabo.App.Core.Common.Domain.Services;
//using Alabo.App.Core.Finance.Domain.CallBacks;
//using Alabo.App.Core.Finance.Domain.Dtos.Transfer;
//using Alabo.App.Core.Finance.Domain.Entities;
//using Alabo.App.Core.Finance.Domain.Entities.Extension;
//using Alabo.App.Core.Finance.Domain.Enums;
//using Alabo.App.Core.Finance.Domain.Repositories;
//using Alabo.App.Core.Finance.ViewModels.Transfer;
//using Alabo.App.Core.Tasks.Domain.Entities;
//using Alabo.App.Core.Tasks.Domain.Enums;
//using Alabo.App.Core.Tasks.Domain.Services;
//using Alabo.App.Core.User;
//using Alabo.App.Core.User.Domain.Callbacks;
//using Alabo.App.Core.User.Domain.Services;
//using Alabo.Framework.Core.Enums.Enum;
//using Alabo.Framework.Core.Extensions;
//using Alabo.Datas.UnitOfWorks;
//using Alabo.Domains.Entities;
//using Alabo.Domains.Enums;
//using Alabo.Domains.Query;
//using Alabo.Domains.Services;
//using Alabo.Extensions;

///// <summary>
///// The Alabo.App.Core.Finance.Domain.Services namespace.{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
///// </summary>
//namespace Alabo.App.Core.Finance.Domain.Services {
//    /// <summary>
//    ///     Class TransferService.
//    /// </summary>
//    public class TransferService : ServiceBase<Transfer, long>, ITransferService {
//        public TransferService(IUnitOfWork unitOfWork) : base(unitOfWork) {
//        }

//        /// <summary>
//        ///     转账
//        /// </summary>
//        /// <param name="view">The 视图.</param>
//        public ServiceResult Add(TransferAddInput view) {
//            #region 转账安全判断，确保数据准备

//            var adminConfig = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
//            if (!adminConfig.IsAllowUserTransfer) {
//                return ServiceResult.FailedWithMessage("当前时间段系统正在进行内部调整,请不要转账。请稍后再试，给您带来的不便深感抱歉!");
//            }

//            var loginUser = Resolve<IUserService>().GetUserDetail(view.UserId);
//            if (loginUser.Status != Status.Normal) {
//                return ServiceResult.FailedWithMessage("转账失败：您的账户不正常，不能进行转账");
//            }

//            if (loginUser.Detail.PayPassword != view.PayPassword.ToMd5HashString()) {
//                return ServiceResult.FailedWithMessage("转账失败：支付密码不正确");
//            }

//            var transferConfig = Resolve<IAutoConfigService>().GetList<TransferConfig>()
//                .FirstOrDefault(r => r.Id == view.TransferConfigId);
//            if (transferConfig == null) {
//                return ServiceResult.FailedWithMessage("转账配置！");
//            }

//            //倍数
//            if (transferConfig.Multiple > 0) {
//                if (view.Amount % transferConfig.Multiple != 0) {
//                    return ServiceResult.FailedWithMessage($"转账金额输入不对，必须是{transferConfig.Multiple}的倍数");
//                }
//            }

//            var OtherUser = Resolve<IUserService>().GetUserDetail(view.OtherUserName);
//            if (OtherUser == null) {
//                return ServiceResult.FailedWithMessage("收款人不存在,请重新输入");
//            }

//            if (OtherUser.Status != Status.Normal) {
//                return ServiceResult.FailedWithMessage("转账失败：收款人账户不正常，已删除或者被冻结");
//            }

//            //如果可以转账给他人
//            //if (transferConfig.CanTransferOther) {
//            //开启推荐关系限制 推荐关系限制
//            if (transferConfig.IsRelation) {
//                var isRelation = false;
//                var loginUserMap = loginUser.Map.ParentMap.Deserialize(new { ParentLevel = 0L, UserId = 0L });
//                if (loginUserMap != null) {
//                    var loginPartertIds = loginUserMap.Select(r => r.UserId);
//                    if (loginPartertIds.Contains(OtherUser.Id)) {
//                        isRelation = true;
//                    }
//                }

//                if (!isRelation) {
//                    var otherUserMap = OtherUser.Map.ParentMap.Deserialize(new { ParentLevel = 0L, UserId = 0L });
//                    if (otherUserMap != null) {
//                        var otherUserParentIds = otherUserMap.Select(r => r.UserId);
//                        if (otherUserParentIds.Contains(loginUser.Id)) {
//                            isRelation = true;
//                        }
//                    }
//                }

//                if (!isRelation) {
//                    return ServiceResult.FailedWithMessage("您选择的转账用户和当前登录用户推荐关系不符合转账要求！");
//                }
//            }

//            //只能转账给自己
//            if (!transferConfig.CanTransferOther && transferConfig.CanTransferSelf) {
//                if (OtherUser.Id != loginUser.Id) {
//                    return ServiceResult.FailedWithMessage("当前配置要求您只能转账给自己！");
//                }
//            }

//            var result = ServiceResult.Success;
//            if (view.Amount <= 0) {
//                return ServiceResult.FailedWithMessage("转账金额必须大于0");
//            }

//            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            var outTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == transferConfig.OutMoneyTypeId);
//            if (outTypeConfig == null) {
//                return ServiceResult.FailedWithMessage("转出货币类型不存在");
//            }

//            var inTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == transferConfig.InMoneyTypeId);
//            if (inTypeConfig == null) {
//                return ServiceResult.FailedWithMessage("收款货币类型不存在");
//            }

//            if (loginUser.Id == OtherUser.Id && outTypeConfig.Id == inTypeConfig.Id) {
//                return ServiceResult.FailedWithMessage("自己不能给自己相同的货币类型账户转账");
//            }

//            if (outTypeConfig.Status != Status.Normal) {
//                return ServiceResult.FailedWithMessage("付款货币类型已删除或被冻结");
//            }

//            if (inTypeConfig.Status != Status.Normal) {
//                return ServiceResult.FailedWithMessage("收款货币类型已删除或被冻结");
//            }

//            var userAccount = Resolve<IAccountService>().GetAccount(loginUser.Id, outTypeConfig.Id);
//            if (userAccount == null) {
//                return ServiceResult.FailedWithMessage("付款账户不存在，转账账户未创建");
//            }

//            if (view.Amount > userAccount.Amount) {
//                return ServiceResult.FailedWithMessage("付款金额大于转账用户账户的余额，请重新输入");
//            }

//            var targetUserAccount = Resolve<IAccountService>().GetAccount(OtherUser.Id, inTypeConfig.Id);
//            if (targetUserAccount == null) {
//                return ServiceResult.FailedWithMessage("收款账户不存在,目标转账未创建");
//            }

//            if (!transferConfig.CanTransferOther) {
//                if (loginUser.Id != OtherUser.Id) {
//                    return ServiceResult.FailedWithMessage("不能转账给它人");
//                }
//            }

//            if (!transferConfig.CanTransferSelf) {
//                if (loginUser.Id == OtherUser.Id) {
//                    return ServiceResult.FailedWithMessage("不能转账给自己");
//                }
//            }

//            #endregion 转账安全判断，确保数据准备

//            //转账用户金额减少
//            userAccount.Amount = userAccount.Amount - view.Amount;

//            var userBill = new Bill {
//                UserId = loginUser.Id,
//                AfterAmount = userAccount.Amount,
//                Amount = view.Amount,
//                OtherUserId = OtherUser.Id,
//                Type = BillActionType.TransferOut,
//                MoneyTypeId = transferConfig.OutMoneyTypeId,
//                Flow = AccountFlow.Spending,
//                CreateTime = DateTime.Now,
//                Intro =
//                    $@"{loginUser.GetUserName()}{outTypeConfig.Name}转账给{OtherUser.GetUserName()}{
//                            inTypeConfig.Name
//                        }账户，金额为{view.Amount} 实际到账{(1 - transferConfig.ServiceFee) * view.Amount} 手续费{
//                            transferConfig.ServiceFee * view.Amount
//                        }"
//            };

//            // 服务费添加
//            targetUserAccount.Amount = targetUserAccount.Amount +
//                                       view.Amount * transferConfig.Fee * (1 - transferConfig.ServiceFee);
//            targetUserAccount.HistoryAmount += view.Amount * transferConfig.Fee * (1 - transferConfig.ServiceFee);
//            var targetBill = new Bill {
//                UserId = OtherUser.Id,
//                AfterAmount = targetUserAccount.Amount,
//                Amount = view.Amount * transferConfig.Fee * (1 - transferConfig.ServiceFee),
//                OtherUserId = loginUser.Id,
//                Type = BillActionType.TransferIn,
//                MoneyTypeId = transferConfig.InMoneyTypeId,
//                Flow = AccountFlow.Income,
//                CreateTime = DateTime.Now,
//                Intro =
//                    $@"{OtherUser.GetUserName()}{inTypeConfig.Name}收到{loginUser.GetUserName()}的{
//                            outTypeConfig.Name
//                        }转账，金额为{view.Amount * transferConfig.Fee * (1 - transferConfig.ServiceFee)}"
//            };

//            var trade = new Trade {
//                Type = TradeType.Transfer,
//                UserId = loginUser.Id,
//                MoneyTypeId = outTypeConfig.Id,
//                Amount = view.Amount,
//                Status = TradeStatus.Success,
//                CreateTime = DateTime.Now
//            };
//            trade.TradeExtension.Transfer = new TradeTransfer {
//                OtherUserId = OtherUser.Id,
//                InMoneyTypeId = inTypeConfig.Id,
//                Amount = targetBill.Amount,
//                ServiceAmount = view.Amount * transferConfig.ServiceFee,
//                TransConfigId = transferConfig.Id
//            };
//            trade.TradeExtension.TradeRemark.UserRemark = view.UserRemark;
//            trade.Extension = trade.TradeExtension.ToJson();

//            //TODO 9月重构注释
//            //var shareOrder = new ShareOrder {
//            //    UserId = loginUser.Id,
//            //    Amount = view.Amount * outTypeConfig.RateFee * transferConfig.ServiceFee,
//            //    Status = ShareOrderStatus.Pending,
//            //    TriggerType = TriggerType.Transfer
//            //};

//            var context = Repository<IBillRepository>().RepositoryContext;
//            context.BeginTransaction();
//            try {
//                Resolve<IAccountService>().Update(userAccount);
//                Resolve<IAccountService>().Update(targetUserAccount);
//                Resolve<ITradeService>().Add(trade);
//                //实体联系改变   //TODO 9月重构注释
//                // shareOrder.EntityId = trade.Id;
//                userBill.EntityId = trade.Id;
//                targetBill.EntityId = trade.Id;
//                Resolve<IBillService>().Add(userBill);
//                Resolve<IBillService>().Add(targetBill);
//                //TODO 9月重构注释
//                // Resolve<IShareOrderService>().Add(shareOrder);
//                context.SaveChanges();
//                context.CommitTransaction();
//            } catch (Exception ex) {
//                context.RollbackTransaction();
//                return ServiceResult.FailedWithMessage("更新失败:" + ex.Message);
//            } finally {
//                context.DisposeTransaction();
//            }

//            return result;
//        }

//        /// <summary>
//        ///     获取s the admin transfer.
//        /// </summary>
//        /// <param name="id">Id标识</param>
//        public ViewAdminTransfer GetAdminTransfer(long id) {
//            var trade = Resolve<ITradeService>().GetSingle(e => e.Id == id && e.Type == TradeType.Transfer);
//            if (trade == null) {
//                return null;
//            }

//            var view = new ViewAdminTransfer();
//            var userGardes = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
//            var moneyConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            view.CheckAmount = trade.Amount;
//            view.User = Resolve<IUserService>().GetSingle(trade.UserId);

//            view.MoneyType = moneyConfigs.SingleOrDefault(e => e.Id == trade.MoneyTypeId);

//            view.TragetMoneyType =
//                moneyConfigs.SingleOrDefault(e => e.Id == trade.TradeExtension.Transfer.InMoneyTypeId);
//            view.TransferConfig = Resolve<IAutoConfigService>().GetList<TransferConfig>()
//                .SingleOrDefault(e => e.Id == trade.TradeExtension.Transfer.TransConfigId);
//            view.TragetUser = Resolve<IUserService>().GetSingle(trade.TradeExtension.Transfer.OtherUserId);

//            view.Serial = trade.Serial;
//            view.Id = trade.Id;
//            view.ServiceFee = trade.TradeExtension.Transfer.ServiceAmount;
//            view.Amoumt = trade.TradeExtension.Transfer.Amount;
//            view.CreateTime = trade.CreateTime;
//            view.Remark = trade.TradeExtension.TradeRemark.UserRemark;
//            view.NextTranfer = Resolve<ITradeService>().Next(trade);
//            view.PrexTranfer = Resolve<ITradeService>().Prex(trade);
//            return view;
//        }

//        /// <summary>
//        ///     获取s the 分页 list.
//        /// </summary>
//        /// <param name="query">查询</param>
//        public PagedList<TransferOutput> GetPageList(object query) {
//            var resultList = Resolve<ITradeService>()
//                .GetPagedList<TransferOutput>(query, r => r.Type == TradeType.Transfer);
//            var moenyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            resultList.ForEach(r => { r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name; });
//            return resultList;
//        }

//        /// <summary>
//        ///     获取s the single. Api 使用
//        /// </summary>
//        /// <param name="id">Id标识</param>
//        /// <param name="userId">用户Id</param>
//        public TransferDetail GetSingle(long id, long userId) {
//            var transferDetai = new TransferDetail();
//            var moneyTypeConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            var trade = Resolve<ITradeService>().GetSingle(e => e.Id == id && e.UserId == userId);
//            var transferConfig = Resolve<IAutoConfigService>().GetList<TransferConfig>();
//            if (trade is null) {
//                return null;
//            }

//            var tragetUser = Resolve<IUserService>().GetSingle(trade.TradeExtension.Transfer.OtherUserId);
//            transferDetai.Account = trade.Amount;
//            transferDetai.Serial = trade.Serial;
//            transferDetai.InMoenyTypeIntr =
//                $"{moneyTypeConfigs.SingleOrDefault(e => e.Id == trade.TradeExtension.Transfer.InMoneyTypeId).Name}[{trade.TradeExtension.Transfer.Amount}]";
//            transferDetai.OutMoenyTypeIntr =
//                $"{moneyTypeConfigs.SingleOrDefault(e => e.Id == trade.MoneyTypeId).Name}[{trade.Amount}]";
//            transferDetai.TragetUserName = $"{tragetUser?.UserName}({tragetUser?.Name})";
//            transferDetai.DateTime = trade.CreateTime.ToString("yyyy-MM-dd hh:ss");
//            transferDetai.TransferConfigIntr = transferConfig
//                .SingleOrDefault(e => e.Id == trade.TradeExtension.Transfer.TransConfigId).Name;
//            transferDetai.Status = trade.Status.GetDisplayName();
//            transferDetai.Message = trade.TradeExtension.TradeRemark.UserRemark;
//            return transferDetai;
//        }

//        /// <summary>
//        ///     获取s the transfer configuration.
//        /// </summary>
//        public IList<KeyValue> GetTransferConfig() {
//            var transfers = Resolve<IAutoConfigService>().GetList<TransferConfig>(e => e.Status == Status.Normal);
//            var dic = new Dictionary<string, object>();
//            transfers.Foreach(r =>
//                dic.Add(r.Id.ToString(), r.Name));

//            var list = new List<KeyValue>();
//            foreach (var item in dic) {
//                var keyValue = new KeyValue {
//                    Value = item.Value,
//                    Key = item.Key
//                };
//                list.Add(keyValue);
//            }

//            return list;
//        }

//        /// <summary>
//        ///     获取s the 会员 list.
//        /// </summary>
//        /// <param name="parameter"></param>
//        public PagedList<Trade> GetUserList(TransferApiInput parameter) {
//            var query = new ExpressionQuery<Trade> {
//                PageIndex = (int)parameter.PageIndex,
//                PageSize = (int)parameter.PageSize
//            };
//            query.And(e => e.UserId == parameter.LoginUserId);
//            query.And(e => e.Type == TradeType.Transfer);
//            query.OrderByDescending(r => r.Id);
//            var model = Resolve<ITradeService>().GetPagedList(query);
//            return model;
//        }

//        /// <summary>
//        ///     转账Edit
//        /// </summary>
//        public ViewHomeTransfer GetView(long id) {
//            var viewHomeTransfer = new ViewHomeTransfer();
//            return viewHomeTransfer;
//        }

//        /// <summary>
//        ///     转账明细
//        /// </summary>
//        /// <param name="query"></param>
//        public PagedList<ViewHomeTransfer> GetUserPage(object query) {
//            var user = query.ToUserObject();
//            var resultList = Resolve<ITradeService>()
//                .GetPagedList<ViewHomeTransfer>(query, r => r.Type == TradeType.Transfer && r.UserId == user.Id);
//            var moenyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            resultList.ForEach(r => {
//                r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name;
//                if (r.TradeExtension != null) {
//                    r.Fee = r.TradeExtension.WithDraw.Fee;
//                }
//            });
//            return resultList;
//        }
//    }
//}

