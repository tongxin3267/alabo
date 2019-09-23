using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.App.Market.BookingSignup.Dtos;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.BookingSignup.Domain.Services
{
    public interface IBookingSignupService : IService<Entities.BookingSignup, ObjectId>
    {

        /// <summary>
        /// 购买
        /// </summary>
        /// <param name="buyInput"></param>
        /// <returns></returns>
        Tuple<BookingBuyOutput, ServiceResult> Buy(BookingBuyInput buyInput);

        /// <summary>
        /// 支付成功回调函数
        /// </summary>
        /// <param name="entityId"></param>
        void AfterPaySuccess(List<object> entityId);
    }
}
