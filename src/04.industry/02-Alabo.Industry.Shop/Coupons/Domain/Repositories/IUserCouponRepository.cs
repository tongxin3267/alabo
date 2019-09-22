using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Shop.Coupons.Domain.Entities;

namespace Alabo.App.Shop.Coupons.Domain.Repositories {
	public interface IUserCouponRepository : IRepository<UserCoupon, ObjectId>  {
	}
}
