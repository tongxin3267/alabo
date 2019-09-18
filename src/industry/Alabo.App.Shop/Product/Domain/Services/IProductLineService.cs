using System.Collections.Generic;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Product.Domain.Services {

    /// <summary>
    ///     Interface IProductLineService
    /// </summary>
    public interface IProductLineService : IService<ProductLine, long> {

        /// <summary>
        ///     获取s the 分页 list.
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ProductLine> GetPageList(object query);

        /// <summary>
        ///     获取s the line models.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pageSize">Size of the 分页.</param>
        /// <param name="pageIndex">Index of the 分页.</param>
        PagedList<ProductLineModel> GetLineModels(string pname, int? pageSize, int? pageIndex);

        /// <summary>
        ///     获取s the line models.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="pageSize">Size of the 分页.</param>
        /// <param name="pageIndex">Index of the 分页.</param>
        PagedList<ProductLineModel> GetLineModels(long id, int? pageSize, int? pageIndex);

        /// <summary>
        ///     获取s the product ids.
        ///     根据产品线Id，获取所有的商品Id
        /// </summary>
        /// <param name="ids">Id标识列表</param>
        List<long> GetProductIds(List<long> ids);
    }
}