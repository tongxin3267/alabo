using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Offline.Order.Domain.Services
{
    public interface IMerchantOrderProductService : IService<MerchantOrderProduct, long>
    {
    }
}