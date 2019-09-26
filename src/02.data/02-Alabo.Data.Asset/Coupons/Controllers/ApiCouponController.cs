using System.Collections.Generic;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.App.Asset.Coupons.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Asset.Coupons.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/Coupon/[action]")]
    public class ApiCouponController : ApiBaseController<Coupon, ObjectId>
    {

        public ApiCouponController() : base()
        {
            BaseService = Resolve<ICouponService>();
        }

        /// <summary>
        /// ����ͱ༭�Ż�ȯ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<Coupon> GetCouponView([FromQuery]string id = "")
        {
            var view = new Coupon();
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<Coupon>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            if (!string.IsNullOrEmpty(id) && !id.Equals("undefined"))
            {
                view = Resolve<ICouponService>().GetSingle(id.ToObjectId());
                if (view != null)
                {
                    if (view.StartPeriodOfValidity.ToString() != "0001/1/1 0:00:00" || view.EndPeriodOfValidity.ToString() != "0001/1/1 0:00:00")
                    {
                        view.AfterDays = -1;
                    }
                }
            }

            return ApiResult.Success(view);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveCoupon([FromBody]Coupon coupon)
        {
            if (coupon == null)
            {
                return ApiResult.Failure("����Ϊ�գ�");
            }

            if (string.IsNullOrEmpty(coupon.Name))
            {
                return ApiResult.Failure("�Ż�ȯ���Ʋ���Ϊ�գ�");
            }
            var serviceResult = Resolve<ICouponService>().EditOrAdd(coupon);

            return ApiResult.Success("����ɹ�");
        }


        /// <summary>
        /// �Ż�ȯ����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CouponDrList>> GetCouponListForDr()
        {
            var list = Resolve<ICouponService>().GetCouponLists();
            return ApiResult.Success(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteCoupon(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return ApiResult.Failure("����idΪ�գ�ɾ��ʧ�ܣ�");
            }
            var result = Resolve<ICouponService>().Delete(id);
            if (result)
            {
                return ApiResult.Success("ɾ���ɹ�!");
            }

            else
            {
                return ApiResult.Failure("ɾ��ʧ�ܣ�");
            }
        }
    }
}