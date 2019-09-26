using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Account;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Core.Finance.Domain.Services {

    public class AccountPayService : ServiceBase, IAccountPayService {

        public AccountPayService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        [Method]
        public void AfterPaySuccess(long tradeId) {
            //            // var ssss = tradeId;
            //            //Resolve<IPayService>().Log(" 执行预留方法:AfterPaySuccess!");
            //            //var input = (RechargeAccountInput)model;
            //            var trade = Resolve<ITradeService>().GetSingle(tradeId);
            //            if (trade == null) {
            //                Resolve<IPayService>().Log($"储值执行结果!trade == null  =>tradeId={tradeId}");
            //                return;
            //            }
            //            var user = Resolve<IUserService>().GetSingle(trade?.UserId);
            //            if (user == null) {
            //                Resolve<IPayService>().Log($"储值执行结果!user==null  => userid={trade?.UserId}");
            //                return;
            //            }
            //            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();
            //            var moneyConfig = moneyConfigs.FirstOrDefault(s => s.Id == MoneyTypeConfig.CNY);

            //            var res = Resolve<IBillService>().Increase(user, moneyConfig, trade.Amount, "会员通过支付宝储值充值");
            //            if (!res.Succeeded)//失败写入日志
            //{
            //                Resolve<IPayService>().Log($"储值执行结果!AfterPaySuccess:{trade.ToJsons()}/{res.ReturnMessage}");
            //            } else {
            //                //支付回调成功
            //                trade.Status = TradeStatus.Success;
            //                Resolve<ITradeService>().AddOrUpdate(trade);
            //            }
        }

        public Task<Tuple<ServiceResult, RechageAccountOutput>> BuyAsync(RechargeAccountInput rechargeAccount) {
            var input = rechargeAccount;

            #region 安全验证

            if (rechargeAccount == null) {
                return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("对象不能为空"), new RechageAccountOutput()));
            }
            if (rechargeAccount.Money <= 0) {
                return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("金额不能小于等于0!"), new RechageAccountOutput()));
            }
            if (rechargeAccount.UserId <= 0) {
                return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("用户id不能为空!"), new RechageAccountOutput()));
            }
            var user = Resolve<IUserService>().GetSingle(rechargeAccount.UserId);
            if (user == null) {
                return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("用户不存在"), new RechageAccountOutput()));
            }

            #endregion 安全验证

            var pay = new Pay {
                Status = PayStatus.WaiPay,
                Type = CheckoutType.Recharge,
                Amount = input.Money,
                UserId = input.UserId,
                //EntityId = tradeInfo.Id.ToString()
            };
            var ids = new List<long>();
            try {
                //TODO 2019年9月25日 重构注释
                //var tradeInfo = new Trade() {
                //    Amount = input.Money,
                //    CreateTime = DateTime.Now,
                //    Extension = string.Empty,
                //    MoneyTypeId = input.MoneyTypeId,
                //    Type = TradeType.Recharge,
                //    Status = TradeStatus.Pending,
                //    UserId = input.UserId,
                //    UserName = string.Empty,
                //};
                //if (Resolve<ITradeService>().Add(tradeInfo)) {
                //    var payExtension = new PayExtension {
                //        TradeNo = tradeInfo.Serial,
                //        Body = $"您正在企牛牛商城上储值，请认真核对账单信息",
                //        UserName = user.GetUserName(),
                //        AfterSuccess = new Linq.Dynamic.BaseServiceMethod {
                //            Method = "AfterPaySuccess",
                //            ServiceName = typeof(IAccountPayService).Name,
                //            Parameter = tradeInfo.Id
                //        },
                //        BuyerCount = 0,
                //        GroupOverId = 0,

                //        //ReturnUrl = $"/pages/index?path=successful_registration&id={tradeNo}",
                //        //NotifyUrl = //不知道什么支付方式
                //        //RedirectUrl = $"/pages/index?path=successful_registration&id={model.Id}",
                //    };
                //    //测试用的
                //    //AfterPaySuccess(payExtension.AfterSuccess.Parameter);
                //    pay.EntityId = $"[{tradeInfo.Id.ToString()}]";
                //    ids.Add(tradeInfo.Id);
                //    //payExtension.RedirectUrl = $"/pages/index?path=successful_registration&id={tradeNo}";

                //    //Linq.Dynamic.DynamicService.ResolveMethod(payExtension.AfterSuccess?.ServiceName,
                //    //                            payExtension.AfterSuccess?.Method,
                //    //                             payExtension.AfterSuccess?.Parameter);

                //    //payExtension.ReturnUrl = "/user/account/wallet";//$"/pages/index?path=successful_registration&id={trade.Id}";
                //    payExtension.RedirectUrl = "/user/account/wallet";// $"/pages/index?path=successful_registration&id={trade.Id}";
                //                                                      // var bookingSignup = Resolve<IBookingSignupService>().GetSingle(u => u.Id == model.BookingId);
                //                                                      // Resolve<ITradeService>().Add(trade);
                //                                                      // pay.EntityId = $"[\"{trade.Id.ToString()}\"]";
                //                                                      // payExtension.TradeNo = trade.Serial;
                //    pay.Extensions = payExtension.ToJsons();

                //    Resolve<IPayService>().Add(pay);
                //} else {
                //    return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("储值失败"), new RechageAccountOutput()));
                //}
            } catch (Exception ex) {
                return Task.FromResult(Tuple.Create(ServiceResult.FailedWithMessage("储值失败" + ex.Message), new RechageAccountOutput()));
                //return ApiResult.Failure<object>("订单记录失败," + ex.Message);
            }
            var outPut = new RechageAccountOutput {
                PayId = pay.Id,
                PayAmount = input.Money,
                OrderIds = ids
            };
            return Task.FromResult(Tuple.Create(ServiceResult.Success, outPut));
        }
    }
}