using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Offline.Order.Domain.Repositories
{
    public interface IMerchantOrderRepository : IRepository<MerchantOrder, long>
    {
    }
}