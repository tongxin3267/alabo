using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Shop.Coupons.Domain.Repositories;

namespace Alabo.App.Shop.Coupons.Domain.Repositories {
	public class UserCouponRepository : RepositoryMongo<UserCoupon, ObjectId>,IUserCouponRepository  {
	public  UserCouponRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
