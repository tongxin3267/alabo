using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Products.Dtos;

namespace Alabo.Industry.Shop.Products.Domain.Services
{
    /// <summary>
    ///     商品统计服务
    /// </summary>
    public interface IProductReportService : IService
    {
        /// <summary>
        ///     获取商品基本统计信息
        /// </summary>
        /// <param name="query"></param>
        PagedList<ProductBaseReport> GetProductBaseReportPage(object query);
    }
}