using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;

namespace Alabo.Industry.Shop.OrderDeliveries.Domain.Repositories
{
    public class OrderDeliveryRepository : RepositoryEfCore<OrderDelivery, long>, IOrderDeliveryRepository
    {
        public OrderDeliveryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}