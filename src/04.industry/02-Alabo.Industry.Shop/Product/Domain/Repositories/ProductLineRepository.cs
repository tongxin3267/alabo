using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    internal class ProductLineRepository : RepositoryEfCore<ProductLine, long>, IProductLineRepository {

        public ProductLineRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}