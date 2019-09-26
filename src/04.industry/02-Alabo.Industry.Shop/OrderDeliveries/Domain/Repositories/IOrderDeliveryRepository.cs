using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;

namespace Alabo.Industry.Shop.OrderDeliveries.Domain.Repositories
{
    public interface IOrderDeliveryRepository : IRepository<OrderDelivery, long>
    {
    }
}