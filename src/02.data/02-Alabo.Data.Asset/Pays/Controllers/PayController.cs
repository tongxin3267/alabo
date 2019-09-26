using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay.Notify;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay.Notify;

namespace Alabo.App.Core.Finance.Controllers {

    [ApiExceptionFilter]
    [Route("Pay/[action]")]
    public class PayController : ApiBaseController {

        /// <summary>
        ///     Initializes a new instance of the
        /// </summary>
        public PayController(
            ) : base() {
        }

        /// <summary>
        ///     手机网站支付异步通知
        /// </summary>
        [HttpPost]
        [Display(Description = "手机网站支付异步通知")]
        public IActionResult WapPayAsync() {
            var log = new Logs {
                Content = "支付宝回调唤起",
                UserId = 1,
                UserName = "admin",
                IpAddress = HttpWeb.Ip,
            };
            try {
                //Resolve<IPayService>().Log("在回调中执行pay中的预留方法  try");
                var config = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
                var _client = new AlipayNotifyClient("RSA2", config.RsaAlipayPublicKey);

                var notify = _client.Execute<AlipayTradeWapPayNotifyResponse>(Request);
                log.Content += ",支付宝回调唤起notify=>" + notify.ToJsons();
                Resolve<ILogsService>().Add(log);

                var aliPayConfig = Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
                //Resolve<IPayService>().Log("在回调中执行pay中的预留方法  aliPayConfig");
                // 返回的商户号要与配置中的相同
                if (aliPayConfig.AppId == notify.AppId) {
                    //Resolve<IPayService>().Log("在回调中执行pay中的预留方法aliPayConfig.AppId == notify.AppId");
                    // 支付状态为成功
                    if ("TRADE_SUCCESS" == notify.TradeStatus) {
                        var payId = notify.PassbackParams.ConvertToLong(0); // 通过支付宝获取订单Id参数
                        var pay = Resolve<IPayService>().GetSingle(payId);
                        //Resolve<IPayService>().Log("在回调中执行pay中的预留方法TRADE_SUCCESS");
                        if (pay != null) {
                            //Resolve<ILogsService>().Log("在回调中执行pay != null");
                            // 如果支付金额同
                            if (pay.Amount.EqualsDigits(notify.ReceiptAmount.ToDecimal())) {
                                // Resolve<ILogsService>().Log("在回调中执行支付金额同");
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = notify.ToJson();
                                pay.ResponseSerial = notify.TradeNo;
                                pay.PayType = PayType.AlipayWap;
                                Resolve<IPayService>().AfterPay(pay, true);
                                //Resolve<IPayService>().Log("在回调中执行pay中的预留方法pay.PayExtension.AfterSuccess != null!");
                                if (pay.PayExtension.AfterSuccess != null) {
                                    //Resolve<IPayService>().Log("在回调中执行pay中的预留方法!");

                                    if (pay.PayExtension.AfterSuccess?.Parameter != null) {
                                        // Resolve<IPayService>().Log($@"在回调中执行pay中的预留方法!有参数{pay.PayExtension.AfterSuccess?.ServiceName}
                                        //    {pay.PayExtension.AfterSuccess?.Method}
                                        //   {pay.PayExtension.AfterSuccess?.Parameter.ToJsons()}");

                                        dynamic a = pay.PayExtension.AfterSuccess?.ServiceName;

                                        Linq.Dynamic.DynamicService.ResolveMethod(pay.PayExtension.AfterSuccess?.ServiceName,
                                            pay.PayExtension.AfterSuccess?.Method,
                                            pay.PayExtension.AfterSuccess?.Parameter);
                                    } else {
                                        //Resolve<IPayService>().Log($@"在回调中执行pay中的预留方法!无参数{pay.PayExtension.AfterSuccess?.ServiceName}
                                        //    {pay.PayExtension.AfterSuccess?.Method}");
                                        Linq.Dynamic.DynamicService.ResolveMethod(pay.PayExtension.AfterSuccess?.ServiceName,
                                           pay.PayExtension.AfterSuccess?.Method);
                                    }
                                }
                            } else {
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = "回调成功，支付金额不相符" + notify.ToJson();
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
                log.Content += $",异常=>{ex.Message}";
                Resolve<ILogsService>().Add(log);
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
                Resolve<IPayService>().Log(notify.ToJson(), LogsLevel.Warning);
                if (notify.ReturnCode == "SUCCESS") {
                    var payId = notify.Attach.ConvertToLong(0); // 通过支付宝获取订单Id参数
                    var pay = Resolve<IPayService>().GetSingle(payId);
                    if (pay != null) {
                        pay.ResponseTime = DateTime.Now;
                        pay.Message = notify.ToJson();
                        pay.ResponseSerial = notify.Attach;
                        pay.PayType = PayType.WeChatPayPublic;
                        var res = Resolve<IPayService>().AfterPay(pay, true);
                        Resolve<IPayService>().Log("微信公众号支付回调结果:" + res.ToJson(), LogsLevel.Warning);
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
        ///     公众号支付异步通知
        /// </summary>
        [HttpPost]
        [Display(Description = "公众号支付异步通知")]
        public async Task<IActionResult> PublicPayAsyncByPayId([FromBody]PayCallBack payInput) {
            Resolve<IPayService>().Log($"PublicPayAsyncByPayId{payInput.ToJson()}");
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
                Resolve<IPayService>().Log("微信公众号支付回调结果:" + res.ToJson(), LogsLevel.Warning);
                return NoContent();
            } catch (Exception ex) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }
    }
}