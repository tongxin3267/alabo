using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.BookingSignup.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Market.BookingSignup.Domain.Repositories;

namespace Alabo.App.Market.BookingSignup.Domain.Repositories {
	public class BookingSignupOrderRepository : RepositoryMongo<BookingSignupOrder, ObjectId>,IBookingSignupOrderRepository  {
	public  BookingSignupOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
