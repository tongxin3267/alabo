using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Shop.Coupons.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Coupons.Domain.Services {
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
