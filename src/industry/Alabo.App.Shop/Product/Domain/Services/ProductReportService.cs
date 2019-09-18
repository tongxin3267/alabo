using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Mapping;

namespace Alabo.App.Shop.Product.Domain.Services {

    /// <summary>
    ///     商品统计服务
    /// </summary>
    public class ProductReportService : ServiceBase, IProductReportService {

        /// <summary>
        ///     获取商品基本统计信息
        /// </summary>
        /// <param name="query"></param>
        public PagedList<ProductBaseReport> GetProductBaseReportPage(object query) {
            var productList = Resolve<IProductService>().GetPagedList(query);
            return AutoMapping.ConverPageList<ProductBaseReport>(productList);
        }

        public ProductReportService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}