using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Services
{
    public interface IBookingSignupOrderService : IService<BookingSignupOrder, ObjectId>
    {
        /// <summary>
        ///     ��Աǩ��
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult UserSign(BookingSignupOrderContact view);
    }
}