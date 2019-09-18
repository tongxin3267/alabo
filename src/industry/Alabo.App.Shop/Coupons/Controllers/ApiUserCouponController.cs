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
        /// ��ָ���û������Ż�ȯ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult SendCoupon(string usersStr, string couponId)
        {
            if(string.IsNullOrEmpty(usersStr))
            {
                return ApiResult.Failure("��������ȷ���û�����");
            }

            if(couponId==null)
            {
                return ApiResult.Failure("��ѡ����Ч���Ż�ȯ��");
            }

          var result= Resolve<IUserCouponService>().Send(usersStr,couponId);

            return ApiResult.Success(result.ReturnMessage);
        }
    }
}