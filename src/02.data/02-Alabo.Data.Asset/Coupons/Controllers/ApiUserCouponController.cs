using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.App.Asset.Coupons.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Coupons.Controllers
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