using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public class OrderDeliveryRepository : RepositoryEfCore<OrderDelivery, long>, IOrderDeliveryRepository {

        public OrderDeliveryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}