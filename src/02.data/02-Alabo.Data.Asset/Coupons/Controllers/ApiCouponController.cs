using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.App.Shop.Coupons.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Coupons.Controllers
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
        /// 保存和编辑优惠券
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
        /// 保存
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveCoupon([FromBody]Coupon coupon)
        {
            if (coupon == null)
            {
                return ApiResult.Failure("数据为空！");
            }

            if (string.IsNullOrEmpty(coupon.Name))
            {
                return ApiResult.Failure("优惠券名称不能为空！");
            }
            var serviceResult = Resolve<ICouponService>().EditOrAdd(coupon);

            return ApiResult.Success("保存成功");
        }


        /// <summary>
        /// 优惠券下拉
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
                return ApiResult.Failure("传入id为空，删除失败！");
            }
            var result = Resolve<ICouponService>().Delete(id);
            if (result)
            {
                return ApiResult.Success("删除成功!");
            }

            else
            {
                return ApiResult.Failure("删除失败！");
            }
        }
    }
}