using Alabo.Domains.Services;
using Alabo.Industry.Shop.Orders.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Domain.Services {

    public interface IOrderProductService : IService<OrderProduct, long> {

        OrderProduct GetSingle(long id);
    }
}