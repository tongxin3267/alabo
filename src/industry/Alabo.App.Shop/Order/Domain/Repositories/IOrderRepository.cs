using System.Collections.Generic;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public interface IOrderRepository : IRepository<Entities.Order, long> {

        List<Entities.Order> GetOrders(int? day = 2);
    }
}