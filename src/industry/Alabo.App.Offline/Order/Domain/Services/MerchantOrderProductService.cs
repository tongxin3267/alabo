using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Order.Domain.Services
{
    public class MerchantOrderProductService : ServiceBase<MerchantOrderProduct, long>, IMerchantOrderProductService
    {
        public MerchantOrderProductService(IUnitOfWork unitOfWork, IRepository<MerchantOrderProduct, long> repository)
            : base(unitOfWork, repository)
        {
        }
    }
}