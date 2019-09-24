using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.App.Market.BookDonae.Domain.Entities;
using Alabo.Domains.Repositories;
using Alabo.Datas.UnitOfWorks;
using  Alabo.App.Market.BookDonae.Domain.Repositories;

namespace Alabo.App.Market.BookDonae.Domain.Repositories {
	public class BooksClassRepository : RepositoryMongo<BooksClass, ObjectId>,IBooksClassRepository  {
	public  BooksClassRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
