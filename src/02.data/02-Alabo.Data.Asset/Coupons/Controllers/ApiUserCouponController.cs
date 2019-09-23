using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.App.Shop.Coupons.Domain.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Coupons.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/UserCoupon/[action]")]
    public class ApiUserCouponController : ApiBaseController<UserCoupon, ObjectId> {

        public ApiUserCouponController() : base()
        {
            BaseService = Resolve<IUserCouponService>();
        }

        /// <summary>
        /// 给指定用户发放优惠券
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SendCoupon(string usersStr, string couponId)
        {
            if(string.IsNullOrEmpty(usersStr))
            {
                return ApiResult.Failure("请输入正确的用户名！");
            }

            if(couponId==null)
            {
                return ApiResult.Failure("请选择有效的优惠券！");
            }

          var result= Resolve<IUserCouponService>().Send(usersStr,couponId);

            return ApiResult.Success(result.ReturnMessage);
        }
    }
}