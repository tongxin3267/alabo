using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Order.Domain.Entities;

namespace Alabo.Industry.Offline.Order.Domain.Repositories
{
    public class MerchantOrderRepository : RepositoryEfCore<MerchantOrder, long>, IMerchantOrderRepository
    {
        public MerchantOrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}