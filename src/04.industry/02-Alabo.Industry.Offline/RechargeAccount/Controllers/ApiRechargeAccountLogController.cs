using System.Collections.Generic;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Helpers;
using Alabo.Industry.Offline.RechargeAccount.Domain.Callbacks;
using Alabo.Industry.Offline.RechargeAccount.Domain.Dtos;
using Alabo.Industry.Offline.RechargeAccount.Entities;
using Alabo.Industry.Offline.RechargeAccount.Services;
using Alabo.Tool.Payment.CallBacks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Offline.RechargeAccount.Controllers {

    [ApiExceptionFilter]
    [Route("Api/RechargeAccountLog/[action]")]
    public class ApiRechargeAccountLogController : ApiBaseController<RechargeAccountLog, ObjectId> {

        public ApiRechargeAccountLogController() :
            base() {
            //_userManager = userManager;
            BaseService = Resolve<IRechargeAccountLogService>();
        }

        [HttpGet]
        public ApiResult GetRechargeConfigList() {
            var list = Ioc.Resolve<IAutoConfigService>().GetConfig(typeof(RechargeAccountConfig).FullName)?.Value
                ?.ToObject<List<RechargeAccountConfig>>();
            var aliPay = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
            var wechatPay = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            var isPay = false;
            if (aliPay.IsEnable || wechatPay.IsEnable) {
                isPay = true;
            }
            return ApiResult.Success(list, (isPay ? 1 : 0).ToString());
        }

        [HttpPost]
        public ApiResult Save([FromBody] RechargeAccountInput model) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            var result = Resolve<IRechargeAccountLogService>().Add(model);
            return ToResult(result);
        }
    }
}