using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Products.Domain.Entities;

namespace Alabo.Industry.Shop.Products.Domain.Repositories {

    public class ProductDetailRepository : RepositoryEfCore<ProductDetail, long>, IProductDetailRepository {

        public ProductDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}