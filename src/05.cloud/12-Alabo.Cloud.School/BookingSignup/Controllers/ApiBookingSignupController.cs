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
        ///     ��������,��Ʒ�����ύ����ʱ����
        ///     �������ﳵ����
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "����������Ʒ�����ύ����ʱ���ã��������ﳵ����")]
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
                return ApiResult.Failure<List<BookingSignupOrderContact>>("Id����Ϊ��");
            }

            var order = Resolve<IBookingSignupOrderService>().GetSingle(u => u.Id == id.ToObjectId() && u.IsPay == true);
            if (order == null) {
                return ApiResult.Failure<List<BookingSignupOrderContact>>("��֧��");
            }
            if (order.Contacts.IsNullOrEmpty()) {
                return ApiResult.Failure<List<BookingSignupOrderContact>>("�����쳣");
            }

            return ApiResult.Success(order.Contacts);
        }

        /// <summary>
        ///     ֧����֧���첽֪ͨ
        /// </summary>
        [HttpPost]
        [Display(Description = "�ֻ���վ֧���첽֪ͨ")]
        public IActionResult WapPayAsync() {
            // var log = new Logs
            //{
            //    Content = "�ԤԼ,֧�����ص�����",
            //    UserId = 1,
            //    UserName = "admin",
            //    IpAddress = HttpWeb.Ip,
            //};
            //Resolve<IBookingSignupOrderService>().Log("�ԤԼ,֧�����ص�����",LogsLevel.Success);
            try {
                var config = Ioc.Resolve<IAutoConfigService>().GetValue<Alabo.App.Core.ApiStore.CallBacks.AlipayPaymentConfig>();
                var _client = new AlipayNotifyClient("RSA2", config.RsaAlipayPublicKey);

                var notify = _client.Execute<AlipayTradeWapPayNotifyResponse>(Request);
                var aliPayConfig = Resolve<IAutoConfigService>().GetValue<Alabo.App.Core.ApiStore.CallBacks.AlipayPaymentConfig>();

                // ���ص��̻���Ҫ�������е���ͬ
                if (aliPayConfig.AppId == notify.AppId) {
                    // ֧��״̬Ϊ�ɹ�
                    if ("TRADE_SUCCESS" == notify.TradeStatus) {
                        var payId = notify.PassbackParams.ConvertToLong(0); // ͨ��֧������ȡ����Id����
                        var pay = Resolve<IPayService>().GetSingle(payId);

                        if (pay != null) {
                            // ���֧�����ͬ
                            if (pay.Amount.EqualsDigits(notify.ReceiptAmount.ToDecimal())) {
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = "�ص��ɹ�," + notify.ToJson();
                                pay.PayType = PayType.AlipayWap;
                                Resolve<IPayService>().AfterPay(pay, true);

                                Resolve<IBookingSignupService>().AfterPaySuccess(new List<object> { pay.PayExtension.TradeNo });
                            } else {
                                pay.ResponseTime = DateTime.Now;
                                pay.Message = "�ص��ɹ�,֧�������." + notify.ToJson();
                                pay.PayType = PayType.AlipayWap;
                                Resolve<IPayService>().AfterPay(pay, false);
                            }
                        }
                        return Content("success", "text/plain");
                    }
                }
                // ���֧��״̬Ϊ�ɹ�

                return NoContent();
            } catch (Exception ex) {
                Resolve<IBookingSignupOrderService>().Log("WapPayAsync=>" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }

        /// <summary>
        ///     ���ں�֧���첽֪ͨ
        /// </summary>
        [HttpPost]
        [Display(Description = "���ں�֧���첽֪ͨ")]
        public async Task<IActionResult> PublicPayAsync() {
            //Resolve<IPayService>().Log("΢�Ź��ں�֧��,��̨�ص�", LogsLevel.Warning);
            try {
                var config = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
                var _client = new WeChatPayNotifyClient(config.APISecretKey);
                var notify = await _client.ExecuteAsync<WeChatPayUnifiedOrderNotifyResponse>(Request);
                //Resolve<IPayService>().Log("΢�Ź��ں�֧���ص�����:" + notify.ToJson(), LogsLevel.Warning);
                if (notify.ReturnCode == "SUCCESS") {
                    var payId = notify.Attach.ConvertToLong(0); // ͨ��֧������ȡ����Id����
                    var pay = Resolve<IPayService>().GetSingle(payId);
                    if (pay != null) {
                        pay.ResponseTime = DateTime.Now;
                        pay.Message = notify.ToJson();
                        pay.ResponseSerial = notify.Attach;
                        pay.PayType = PayType.WeChatPayPublic;
                        var res = Resolve<IPayService>().AfterPay(pay, true);
                        Resolve<IBookingSignupService>().AfterPaySuccess(new List<object> { pay.PayExtension.TradeNo });
                        //Resolve<IPayService>().Log("΢�Ź��ں�֧���ص����:" + res.ToJson(), LogsLevel.Warning);
                    }
                    if (notify.ResultCode == "SUCCESS") {
                        return Content("<xml><return_code><![CDATA[SUCCESS]]></return_code></xml>", "text/xml");
                    }
                }

                return NoContent();
            } catch (Exception ex) {
                Resolve<IPayService>().Log("΢�Ź��ں�֧���쳣" + ex.Message, LogsLevel.Error);
                return NoContent();
            }
        }

        /// <summary>
        ///     ΢��֧���첽֪ͨ
        /// </summary>
        [HttpPost]
        [Display(Description = "���ں�֧���첽֪ͨ")]
        public async Task<IActionResult> PublicPayAsyncByPayId([FromBody]PayCallBack payInput) {
            //Resolve<IBookingSignupOrderService>().Log($"�ԤԼ,΢�Żص�����PublicPayAsyncByPayId{payInput.ToJson()}", LogsLevel.Success);

            if (payInput == null) {
                return NoContent();
            }
            var pay = Resolve<IPayService>().GetSingle(payInput.PayId);
            if (pay == null) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  pay ����Ϊnull");
                return NoContent();
            }
            if (pay.UserId != payInput.UserId) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  useId ��һ��");
                return NoContent();
            }
            if (payInput.ClientType != Alabo.Framework.Core.Enums.Enum.ClientType.WeChat) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId ��΢�Ź��ں�");
                return NoContent();
            }
            var user = Resolve<IUserService>().GetUserDetail(payInput.UserId);
            if (user == null) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId  user ����Ϊ��");
                return NoContent();
            }
            if (user.Detail.OpenId.ToLower() != payInput.OpenId.ToLower()) {
                Resolve<IPayService>().Log("PublicPayAsyncByPayId openId ��һ��");
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