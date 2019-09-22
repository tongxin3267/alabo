using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public interface IOrderDeliveryRepository : IRepository<OrderDelivery, long> {
    }
}