using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;

using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.App.Market.BookingSignup.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Market.BookingSignup.Controllers {

    [ApiExceptionFilter]
    [Route("Api/BookingSignupOrder/[action]")]
    public class ApiBookingSignupOrderController : ApiBaseController<BookingSignupOrder, ObjectId> {

        public ApiBookingSignupOrderController() : base() {
            BaseService = Resolve<IBookingSignupOrderService>();
        }

        /// <summary>
        /// 会员签到
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult UserSign(BookingSignupOrderContact model) {
            if (model.Name.IsNullOrEmpty()) {
                return ApiResult.Failure("姓名不能为空");
            }

            if (model.Mobile.IsNullOrEmpty()) {
                return ApiResult.Failure("手机号不能为空");
            }

            var result = Resolve<IBookingSignupOrderService>().UserSign(model);
            return ApiResult.Success(result);
        }
    }
}