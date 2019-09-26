using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Repositories
{
    public class CouponRepository : RepositoryMongo<Coupon, ObjectId>, ICouponRepository
    {
        public CouponRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}