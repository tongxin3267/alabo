using System.Collections.Generic;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Services {
    public interface ICouponService : IService<Coupon, ObjectId>
    {
        /// <summary>
        /// 编辑新增
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        ServiceResult EditOrAdd(Coupon coupon);

        /// <summary>
        /// 获取优惠券下拉列表
        /// </summary>
        /// <returns></returns>
        List<CouponDrList> GetCouponLists();
    }
}
