using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Orders.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Domain.Services {

    public class OrderProductService : ServiceBase<OrderProduct, long>, IOrderProductService {

        public OrderProduct GetSingle(long id) {
            var orderproduct = Resolve<IOrderProductService>().GetSingle(r => r.OrderId == id);
            return orderproduct;
        }

        public OrderProductService(IUnitOfWork unitOfWork, IRepository<OrderProduct, long> repository) : base(unitOfWork, repository) {
        }
    }
}