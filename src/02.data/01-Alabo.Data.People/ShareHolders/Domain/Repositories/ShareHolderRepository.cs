using Alabo.Data.People.ShareHolders.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.ShareHolders.Domain.Repositories {
	public class ShareHolderRepository : RepositoryMongo<ShareHolder, ObjectId>,IShareHolderRepository  {
	public  ShareHolderRepository(IUnitOfWork unitOfWork) : base(unitOfWork){
	}
	}
}
