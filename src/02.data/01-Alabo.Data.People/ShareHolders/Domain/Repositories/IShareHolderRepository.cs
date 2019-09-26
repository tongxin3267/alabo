using Alabo.Data.People.ShareHolders.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.ShareHolders.Domain.Repositories {
	public interface IShareHolderRepository : IRepository<ShareHolder, ObjectId>  {
	}
}
