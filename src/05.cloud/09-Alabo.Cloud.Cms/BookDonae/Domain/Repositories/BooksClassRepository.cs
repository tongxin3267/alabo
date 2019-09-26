using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Repositories {
	public class BooksClassRepository : RepositoryMongo<BooksClass, ObjectId>,IBooksClassRepository  {
	public  BooksClassRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
