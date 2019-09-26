using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Repositories {
	public interface IUserCouponRepository : IRepository<UserCoupon, ObjectId>  {
	}
}
