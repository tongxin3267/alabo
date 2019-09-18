using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Product.Domain.Services {

    public class ProductDetailService : ServiceBase<ProductDetail, long>, IProductDetailService {

        public ProductDetailService(IUnitOfWork unitOfWork, IRepository<ProductDetail, long> repository) : base(unitOfWork, repository) {
        }
    }
}