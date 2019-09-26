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
        /// ��Աǩ��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult UserSign(BookingSignupOrderContact model) {
            if (model.Name.IsNullOrEmpty()) {
                return ApiResult.Failure("��������Ϊ��");
            }

            if (model.Mobile.IsNullOrEmpty()) {
                return ApiResult.Failure("�ֻ��Ų���Ϊ��");
            }

            var result = Resolve<IBookingSignupOrderService>().UserSign(model);
            return ApiResult.Success(result);
        }
    }
}