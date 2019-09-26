using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Repositories {
	public class BookingSignupOrderRepository : RepositoryMongo<BookingSignupOrder, ObjectId>,IBookingSignupOrderRepository  {
	public  BookingSignupOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
