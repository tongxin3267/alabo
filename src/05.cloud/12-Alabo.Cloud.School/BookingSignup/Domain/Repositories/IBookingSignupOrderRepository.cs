using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Repositories {
	public interface IBookingSignupOrderRepository : IRepository<BookingSignupOrder, ObjectId>  {
	}
}
