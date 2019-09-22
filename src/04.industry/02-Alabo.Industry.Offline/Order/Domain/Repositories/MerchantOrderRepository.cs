using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Order.Domain.Repositories
{
    public class MerchantOrderRepository : RepositoryEfCore<MerchantOrder, long>, IMerchantOrderRepository
    {
        public MerchantOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}