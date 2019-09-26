using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;

namespace Alabo.Industry.Shop.Deliveries.Domain.Repositories
{
    public class StoreRepository : RepositoryEfCore<Store, long>, IStoreRepository
    {
        public StoreRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}