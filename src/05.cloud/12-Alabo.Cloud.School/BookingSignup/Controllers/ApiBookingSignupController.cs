using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.App.Market.BookingSignup.Domain.Services;
using Alabo.App.Market.BookingSignup.Dtos;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay.Notify;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay.Notify;

namespace Alabo.App.Market.BookingSignup.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BookingSignup/[action]")]
    public class ApiBookingSignupController : ApiBaseController<Domain.Entities.BookingSignup, ObjectId> {

        public ApiBookingSignupController() : base() {
            BaseService = Resolve<IBookingSignupService>();
        }

        /// <summary>
        ///     立即购买,商品够，提交订单时候用
        ///     包括购物车购买
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "立即购买，商品购买，提交订单时候用，包括购物车购买")]
        [ApiAuth]
        public ApiResult<Dtos.BookingBuyOutput> Buy([FromBody] BookingBuyInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<Dtos.BookingBuyOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IBookingSignupService>().Buy(parameter);
            if (!result.Item2.Succeeded) {
                return ApiResult.Failure<Dtos.BookingBuyOutput>(result.Item2.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item1);
        }

        [HttpGet]
        public ApiResult<List<BookingSignupOrderContact>> GetOrderUserByOrderId(string id) {
            if (id.IsNullOrEmpty()) {
                return ApiResult.Failure<List<BookingSignupOrderContact>>("Id不能为空");
            }

            var order = Resolve<IBookingSignupOrderService>().GetSingle(u => u.Id == id.ToObjectId() && u.IsPay == true);
            if (order == null) {
                return ApiResult.Failure<List<BookingSignupOrderContact>>("请支付");
            }
            if (order.Contacts.IsNullOrEmpty()) {
                return ApiResult.Failure<List<BookingSignupOrderContact>>("数据异常");
            }

            return ApiResult.Success(order.Contacts);
        }

        /// <summary>
        ///     支付宝支付异步通知
        /// </summary>
        [HttpPost]
        [Display(Description = "手机网站支付异步通知")]
        public IActionResult WapPayAsync() {
            // var log = new Logs
            //{
            //    Content = "活动预约,支付宝回调唤起",
            //    UserId = 1,
            //    UserName = "admin",
            //    IpAddress = HttpWeb.Ip,
            //};
            //Resolve<IBookingSignupOrderService>().Log("活动预约,支付宝回调唤起",LogsLevel.Success);
            try {
                var config = Ioc.Resolve<IAutoConfigService>().GetValue<Alabo.App.Core.ApiStore.CallBacks.AlipayPaymentConfig>();
                var _client = new AlipayNotifyClient("RSA2", config.RsaAlipayPublicKey);

                var notify = _client.Execute<AlipayTradeWapPayNotifyResponse>(Request);
                var aliPayConfig = Resolve<IAutoConfigService>().GetValue<Alabo.App.Core.ApiStore.CallBacks.AlipayPaymentConfig>();

                // 返回的商户号要与配置中的相同
                if (aliPayConfig.AppId == notify.AppId) {
                    // 支付状态为成功
                    if ("TRADE_SUCCESS" == notify.TradeStatus) {
                        var payId = notify.PassbackParams.ConvertToLong(0); // 通过支付宝获取订单Id参数
                        var pay = Resolve<IPayService>().GetSingle(payId);

                        if (pay != null) {
                            // 如果支付金额同
                            if (pay.Amount.EqualsDigits(notify.ReceiptAmount.ToDecimal())) {
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = "回调成功," + notify.ToJson();
                                pay.PayType = PayType.AlipayWap;
                                Resolve<IPayService>().AfterPay(pay, true);

                                Resolve<IBookingSignupService>().AfterPaySuccess(new List<object> { pay.PayExtension.TradeNo });
                            } else {
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = "回调成功,支付金额不相符." + notify.ToJson();
                                pay.PayType = PayType.AlipayWap;
                                Resolve<IPayService>().AfterPay(pay, false);
                            }
                        }
                        return Content("success", "text/plain");
                    }
                }
                // 如果支付状态为成功

                return NoContent();
            } catch (Exception ex) {
                Resolve<IBookingSignupOrderService>().Log("WapPayAsync=>" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }

        /// <summary>
        ///     公众号支付异步通知
        /// </summary>
        [HttpPost]
        [Display(Description = "公众号支付异步通知")]
        public async Task<IActionResult> PublicPayAsync() {
            //Resolve<IPayService>().Log("微信公众号支付,后台回调", LogsLevel.Warning);
            try {
                var config = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
                var _client = new WeChatPayNotifyClient(config.APISecretKey);
                var notify = await _client.ExecuteAsync<WeChatPayUnifiedOrderNotifyResponse>(Request);
                //Resolve<IPayService>().Log("微信公众号支付回调参数:" + notify.ToJson(), LogsLevel.Warning);
                if (notify.ReturnCode == "SUCCESS") {
                    var payId = notify.Attach.ConvertToLong(0); // 通过支付宝获取订单Id参数
                    var pay = Resolve<IPayService>().GetSingle(payId);
                    if (pay != null) {
                        pay.ResponseTime = DateTime.Now;
                        pay.Message = notify.ToJson();
                        pay.ResponseSerial = notify.Attach;
                        pay.PayType = PayType.WeChatPayPublic;
                        var res = Resolve<IPayService>().AfterPay(pay, true);
                        Resolve<IBookingSignupService>().AfterPaySuccess(new List<object> { pay.PayExtension.TradeNo });
                        //Resolve<IPayService>().Log("微信公众号支付回调结果:" + res.ToJson(), LogsLevel.Warning);
                    }
                    if (notify.ResultCode == "SUCCESS") {
                        return Content("<xml><return_code><![CDATA[SUCCESS]]></return_code></xml>", "text/xml");
                    }
                }

                return NoContent();
            } catch (Exception ex) {
                Resolve<IPayService>().Log("微信公众号支付异常" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }

        /// <summary>
        ///     微信支付异步通知
        /// </summary>
        [HttpPost]
        [Display(Description = "公众号支付异步通知")]
        public async Task<IActionResult> PublicPayAsyncByPayId([FromBody]PayCallBack payInput) {
            //Resolve<IBookingSignupOrderService>().Log($"活动预约,微信回调唤起PublicPayAsyncByPayId{payInput.ToJson()}", LogsLevel.Success);

            if (payInput == null) {
                return NoContent();
            }
            var pay = Resolve<IPayService>().GetSingle(payInput.PayId);
            if (pay == null) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  pay 不能为null");
                return NoContent();
            }
            if (pay.UserId != payInput.UserId) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  useId 不一样");
                return NoContent();
            }
            if (payInput.ClientType != Alabo.Framework.Core.Enums.Enum.ClientType.WeChat) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId 非微信公众号");
                return NoContent();
            }
            var user = Resolve<IUserService>().GetUserDetail(payInput.UserId);
            if (user == null) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  user 不能为空");
                return NoContent();
            }
            if (user.Detail.OpenId.ToLower() != payInput.OpenId.ToLower()) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId openId 不一致");
                return NoContent();
            }

            try {
                pay.ResponseTime = DateTime.Now;
                //pay.Message = notify.ToJson();
                //pay.ResponseSerial = notify.TradeNo;
                pay.PayType = PayType.WeChatPayPublic;
                var res = Resolve<IPayService>().AfterPay(pay, true);
                Resolve<IBookingSignupService>().AfterPaySuccess(new List<object> { pay.PayExtension.TradeNo });
                return NoContent();
            } catch (Exception ex) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }
    }
}