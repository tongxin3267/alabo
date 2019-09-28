using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Dtos;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Industry.Shop.Products.ViewModels;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Products.Domain.Services
{
    public interface IProductService : IService<Product, long>
    {
        /// <summary>
        ///     获取产品数量
        /// </summary>
        /// <returns></returns>
        long GetProductCount();

        /// <summary>
        ///     通过店铺获取产品数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long GetProductByStoreCount(ObjectId storeId);

        /// <summary>
        ///     get time limit buy products
        /// </summary>
        List<TimeLimitBuyItem> GetTimeLimitBuyList();

        /// <summary>
        ///     获取商品分类
        /// </summary>
        List<Relation> GetProductClassList();

        /// <summary>
        /// </summary>
        /// <returns></returns>
        List<Relation> GetProductRelations();

        /// <summary>
        ///     根据分类Id获取商品列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        List<Product> GetProductsByRelationId(long relationId);

        /// <summary>
        ///     获取推荐商品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IList<ProductItem> GetRecommendProduct(long productId);

        /// <summary>
        ///     根据商品ID获取店铺信息
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        StoreInfoOutput GetStoreInfoByProductId(long productId);

        /// <summary>
        ///     商品查询列表，次函数基本完成了商品列表的所有功能
        /// </summary>
        /// <param name="productApiInput"></param>
        ProductItemApiOutput GetProductItems(ProductApiInput productApiInput);

        /// <summary>
        ///     商品查询列表，次函数基本完成了商品列表的所有功能 异步
        /// </summary>
        /// <param name="productApiInput"></param>
        Task<ProductItemApiOutput> GetProductItemsAsync(ProductApiInput productApiInput);

        /// <summary>
        ///     获取前台商品详情
        ///     只能用在前台商品详情中
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="userId"></param>
        Product GetShow(long id, long userId);

        /// <summary>
        ///     商品价格的最终显示方式，整个系统显示的价格都由改服务计算
        /// </summary>
        /// <param name="price">价格</param>
        /// <param name="priceStyleId">商城模式Id</param>
        /// <param name="productMinCashRate">商品自定义，现金抵扣比例</param>
        string GetDisplayPrice(decimal price, Guid priceStyleId, decimal productMinCashRate);

        /// <summary>
        ///     Shows the specified identifier.
        ///     获取商品详情
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="userId"></param>
        Tuple<ServiceResult, Product> Show(long id, long userId);
    }
}