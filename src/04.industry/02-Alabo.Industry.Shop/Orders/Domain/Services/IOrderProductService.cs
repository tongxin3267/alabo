using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Order.Domain.Services {

    public interface IOrderProductService : IService<OrderProduct, long> {

        OrderProduct GetSingle(long id);
    }
}