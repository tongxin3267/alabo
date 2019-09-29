using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.App.Asset.Coupons.Domain.Services
{
    public class CouponService : ServiceBase<Coupon, ObjectId>, ICouponService
    {
        public CouponService(IUnitOfWork unitOfWork, IRepository<Coupon, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     �����༭�Ż�ȯ
        /// </summary>
        /// <param name="coupon"></param>
        /// <returns></returns>
        public ServiceResult EditOrAdd(Coupon coupon)
        {
            if (coupon == null) return ServiceResult.FailedMessage("�������Ϊ��");
            if (coupon.Type == CouponType.Reduce)
                if (coupon.MinOrderPrice <= coupon.Value)
                    return ServiceResult.FailedMessage("�������ֻ��С�����Ѷ�");
            if (coupon.TimeLimit == CouponTimeLimit.Days)
                if (coupon.AfterDays <= 0)
                    return ServiceResult.FailedMessage("��ú���Ч�������������0");

            var model = coupon;
            var result = AddOrUpdate(model);
            if (result)
                return ServiceResult.Success;
            return ServiceResult.Failed;
        }

        /// <summary>
        ///     ����ѡ��
        /// </summary>
        /// <returns></returns>
        public List<CouponDrList> GetCouponLists()
        {
            var list = new List<CouponDrList>();
            var results = Resolve<ICouponService>().GetList();

            if (results != null)
                foreach (var item in results)
                    list.Add(new CouponDrList
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
            return list;
        }
    }
}