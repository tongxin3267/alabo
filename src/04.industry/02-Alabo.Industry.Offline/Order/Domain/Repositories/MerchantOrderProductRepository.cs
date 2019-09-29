using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Order.Domain.Entities;

namespace Alabo.Industry.Offline.Order.Domain.Repositories
{
    public class MerchantOrderProductRepository : RepositoryEfCore<MerchantOrderProduct, long>,
        IMerchantOrderProductRepository
    {
        public MerchantOrderProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}