using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Repositories;
using Alabo.App.Market.BookDonae.Domain.Entities;

namespace Alabo.App.Market.BookDonae.Domain.Repositories {
	public interface IBooksClassRepository : IRepository<BooksClass, ObjectId>  {
	}
}
