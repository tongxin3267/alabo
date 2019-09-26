using System.Collections.Generic;
using Alabo.Domains.Repositories;

namespace Alabo.Industry.Shop.Orders.Domain.Repositories {

    public interface IOrderRepository : IRepository<Entities.Order, long> {

        List<Entities.Order> GetOrders(int? day = 2);
    }
}