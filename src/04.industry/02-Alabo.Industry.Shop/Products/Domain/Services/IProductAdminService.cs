using Microsoft.AspNetCore.Http;
using System;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Product.Domain.Services {

    /// <summary>
    ///     商品后台操作方法写到此处
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.IService" />
    public interface IProductAdminService : IService {

        /// <summary>
        ///     获取商品编辑页面详情
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <param name="storeId">The store identifier.</param>
        /// <returns>ViewProductEdit.</returns>
        ViewProductEdit GetViewProductEdit(long productId, long storeId);

        /// <summary>
        ///     保存商品
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <returns>ServiceResult.</returns>
        ServiceResult AddOrUpdate(ViewProductEdit view, HttpRequest httpRequest);

        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        void Delete(long id);

        /// <summary>
        ///     编辑出错时使用
        /// </summary>
        /// <param name="view"></param>
        ViewProductEdit GetPageView(ViewProductEdit view);

        /// <summary>
        ///     后台商品搜索
        /// </summary>
        /// <param name="query"></param>

        PagedList<ViewProductList> GetViewProductList(object query);

        /// <summary>
        ///     类目下是否有商品
        /// </summary>
        /// <param name="categoryId"></param>

        bool CheckCategoryHasProduct(Guid categoryId);
    }
}