using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.Industry.Shop.Deliveries.Domain.Repositories {

    public class StoreRepository : RepositoryEfCore<Entities.Store, long>, IStoreRepository {

        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}