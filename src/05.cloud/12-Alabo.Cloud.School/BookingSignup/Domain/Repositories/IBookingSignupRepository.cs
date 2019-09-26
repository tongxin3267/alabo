using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Repositories {
	public interface IBookingSignupRepository : IRepository<Entities.BookingSignup, ObjectId>  {
	}
}
