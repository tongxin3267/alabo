using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Store.Domain.Repositories {

    public class StoreRepository : RepositoryEfCore<Entities.Store, long>, IStoreRepository {

        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}