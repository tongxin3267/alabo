using Alabo.Domains.Repositories;
using Alabo.Industry.Offline.Order.Domain.Entities;

namespace Alabo.Industry.Offline.Order.Domain.Repositories
{
    public interface IMerchantOrderRepository : IRepository<MerchantOrder, long>
    {
    }
}