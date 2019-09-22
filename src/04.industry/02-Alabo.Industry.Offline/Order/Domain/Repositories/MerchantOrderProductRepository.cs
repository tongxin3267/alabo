using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Order.Domain.Repositories
{
    public class MerchantOrderProductRepository : RepositoryEfCore<MerchantOrderProduct, long>, IMerchantOrderProductRepository
    {
        public MerchantOrderProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}