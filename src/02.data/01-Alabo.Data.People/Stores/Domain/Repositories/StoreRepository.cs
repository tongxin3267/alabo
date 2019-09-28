using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Repositories
{
    public class StoreRepository : RepositoryMongo<Store, ObjectId>, IStoreRepository
    {
        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}