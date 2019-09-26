using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Offline.Order.Domain.Entities;

namespace Alabo.Industry.Offline.Order.Domain.Services
{
    public class MerchantOrderProductService : ServiceBase<MerchantOrderProduct, long>, IMerchantOrderProductService
    {
        public MerchantOrderProductService(IUnitOfWork unitOfWork, IRepository<MerchantOrderProduct, long> repository)
            : base(unitOfWork, repository)
        {
        }
    }
}