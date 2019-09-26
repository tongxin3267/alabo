using System;
using System.Collections.Generic;
using Alabo.Cloud.School.BookingSignup.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Services
{
    public interface IBookingSignupService : IService<Entities.BookingSignup, ObjectId>
    {

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="buyInput"></param>
        /// <returns></returns>
        Tuple<BookingBuyOutput, ServiceResult> Buy(BookingBuyInput buyInput);

        /// <summary>
        /// ֧���ɹ��ص�����
        /// </summary>
        /// <param name="entityId"></param>
        void AfterPaySuccess(List<object> entityId);
    }
}
