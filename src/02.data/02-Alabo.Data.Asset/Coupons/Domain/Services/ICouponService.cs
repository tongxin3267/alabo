using System.Collections.Generic;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Services {
    public interface ICouponService : IService<Coupon, ObjectId>
    {
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        ServiceResult EditOrAdd(Coupon coupon);

        /// <summary>
        /// ��ȡ�Ż�ȯ�����б�
        /// </summary>
        /// <returns></returns>
        List<CouponDrList> GetCouponLists();
    }
}
