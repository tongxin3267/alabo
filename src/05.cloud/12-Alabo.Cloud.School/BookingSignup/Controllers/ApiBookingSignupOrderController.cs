using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Cloud.School.BookingSignup.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.School.BookingSignup.Controllers {

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