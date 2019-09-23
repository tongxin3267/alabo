using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Shop.Store.Domain.Repositories {

    public class StoreRepository : RepositoryEfCore<Entities.Store, ObjectId>, IStoreRepository {

        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}