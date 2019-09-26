using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Repositories {
	public class BookDonaeInfoRepository : RepositoryMongo<BookDonaeInfo, ObjectId>,IBookDonaeInfoRepository  {
	public  BookDonaeInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
