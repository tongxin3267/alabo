using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Orders.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {
        List<Order> GetOrders(int? day = 2);
    }
}