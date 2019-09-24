using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.BookingSignup.Domain.Services {

    public interface IBookingSignupOrderService : IService<BookingSignupOrder, ObjectId> {

        /// <summary>
        /// ª·‘±«©µΩ
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult UserSign(BookingSignupOrderContact view);
    }
}