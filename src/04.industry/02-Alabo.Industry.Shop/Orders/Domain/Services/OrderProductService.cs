using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Order.Domain.Services {

    public class OrderProductService : ServiceBase<OrderProduct, long>, IOrderProductService {

        public OrderProduct GetSingle(long id) {
            var orderproduct = Resolve<IOrderProductService>().GetSingle(r => r.OrderId == id);
            return orderproduct;
        }

        public OrderProductService(IUnitOfWork unitOfWork, IRepository<OrderProduct, long> repository) : base(unitOfWork, repository) {
        }
    }
}