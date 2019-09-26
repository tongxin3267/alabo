using System;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Categories.Domain.Services {

    public interface ICategoryService : IService<Entities.Category, Guid> {

        /// <summary>
        ///     根据Guid获取商品类目
        /// </summary>
        /// <param name="guid"></param>
        Entities.Category GetSingle(Guid guid);

        /// <summary>
        ///     处理商品属性
        ///     编辑或删除处理商品属性
        /// </summary>
        /// <param name="product"></param>
        /// <param name="request"></param>
        string AddOrUpdateOrDeleteProductCategoryData(Product product, HttpRequest request);

        Tuple<ServiceResult, Entities.Category> Delete(Guid id);
    }
}