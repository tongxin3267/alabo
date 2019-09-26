using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Repositories
{
    public class BookingSignupRepository : RepositoryMongo<Entities.BookingSignup, ObjectId>, IBookingSignupRepository
    {
        public BookingSignupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}