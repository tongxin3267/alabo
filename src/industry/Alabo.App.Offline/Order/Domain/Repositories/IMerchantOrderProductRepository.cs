using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Order.Domain.Repositories
{
    public interface IMerchantOrderProductRepository : IRepository<MerchantOrderProduct, long>
    {
    }
}