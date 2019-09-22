using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Market.BookingSignup.Domain.Entities;

namespace Alabo.App.Market.BookingSignup.Domain.Repositories {
	public interface IBookingSignupRepository : IRepository<Entities.BookingSignup, ObjectId>  {
	}
}
