using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    public interface IProductLineRepository : IRepository<ProductLine, long> {
    }
}