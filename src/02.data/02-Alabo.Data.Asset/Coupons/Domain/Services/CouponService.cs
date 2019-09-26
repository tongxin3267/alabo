using System.Collections.Generic;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Services
{
    public class CouponService : ServiceBase<Coupon, ObjectId>, ICouponService
    {
        public CouponService(IUnitOfWork unitOfWork, IRepository<Coupon, ObjectId> repository) : base(unitOfWork, repository)
        {
        }

        /// <summary>
        /// 保存或编辑优惠券
        /// </summary>
        /// 
        /// <param name="coupon"></param>
        /// <returns></returns>
        public ServiceResult EditOrAdd(Coupon coupon)
        {
            if (coupon == null)
            {
                return ServiceResult.FailedMessage("传入参数为空");
            }
            if (coupon.Type == CouponType.Reduce)
            {
                if (coupon.MinOrderPrice <= coupon.Value)
                {
                    return ServiceResult.FailedMessage("立减金额只能小于消费额");
                }
            }
            if(coupon.TimeLimit== CouponTimeLimit.Days)
            {
                if(coupon.AfterDays<=0)
                {
                    return ServiceResult.FailedMessage("获得后有效期天数必须大于0");
                }
            }

            Coupon model = coupon;
            var result = AddOrUpdate(model);
            if (result) {
                return ServiceResult.Success;
            } else {
                return ServiceResult.Failed;
            }
        }

        /// <summary>
        /// 下拉选择
        /// </summary>
        /// <returns></returns>
        public List<CouponDrList> GetCouponLists()
        {
            List<CouponDrList> list = new List<CouponDrList>();
            var results = Resolve<ICouponService>().GetList();

            if (results != null)
            {
                foreach (var item in results)
                {
                    list.Add(new CouponDrList
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
            return list;
        }

    }
}
