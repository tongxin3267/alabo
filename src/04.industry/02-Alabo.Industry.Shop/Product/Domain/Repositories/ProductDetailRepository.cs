using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    public class ProductDetailRepository : RepositoryEfCore<ProductDetail, long>, IProductDetailRepository {

        public ProductDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}