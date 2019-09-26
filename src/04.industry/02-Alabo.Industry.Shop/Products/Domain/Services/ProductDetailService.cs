using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Products.Domain.Entities;

namespace Alabo.Industry.Shop.Products.Domain.Services
{
    public class ProductDetailService : ServiceBase<ProductDetail, long>, IProductDetailService
    {
        public ProductDetailService(IUnitOfWork unitOfWork, IRepository<ProductDetail, long> repository) : base(
            unitOfWork, repository)
        {
        }
    }
}