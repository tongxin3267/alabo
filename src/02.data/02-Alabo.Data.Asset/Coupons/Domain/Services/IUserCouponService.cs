using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Services {
	public interface IUserCouponService : IService<UserCoupon, ObjectId>
    {

        /// <summary>
        /// ����û����ö��Ÿ���
        /// </summary>
        /// <param name="usersStr"></param>
        /// <returns></returns>
        ServiceResult Send(string usersStr,string couponId);
    }
	}
