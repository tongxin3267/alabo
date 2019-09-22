using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.Domains.Entities;
using System.Collections.Generic;

namespace Alabo.App.Shop.Coupons.Domain.Services {
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
