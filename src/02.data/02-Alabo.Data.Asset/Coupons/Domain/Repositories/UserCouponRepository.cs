using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Repositories
{
    public class UserCouponRepository : RepositoryMongo<UserCoupon, ObjectId>, IUserCouponRepository
    {
        public UserCouponRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}