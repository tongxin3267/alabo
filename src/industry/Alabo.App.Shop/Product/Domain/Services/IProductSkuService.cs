using System.Collections.Generic;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Product.Domain.Services
{

    public interface IProductSkuService : IService<ProductSku, long>
    {

        /// <summary>
        ///     更新、添加、或删除商品SKU
        /// </summary>
        /// <param name="product"></param>
        /// <param name="skuList"></param>
        ServiceResult AddUpdateOrDelete(Entities.Product product, List<ProductSku> skuList);

        /// <summary>
        ///     Gets the store money buy skus.
        ///     根据sku信息，获取店铺可是使用的资产信息
        /// </summary>
        /// <param name="productSkuItems">The product sku items.</param>
        IEnumerable<OrderMoneyItem> GetStoreMoneyBuySkus(IEnumerable<ProductSkuItem> productSkuItems, long UserId);

        /// <summary>
        ///     Automatics the update sku price.
        ///     后台自动更新商品Sku的价格
        ///     更加货币类型和价格自动更新显示价、最高现金价
        /// </summary>
        void AutoUpdateSkuPrice(long productId = 0);

        /// <summary>
        /// 获取会员等级价
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        List<ProductSku> GetGradePrice(long productId);

        /// <summary>
        /// 更新会员等级价
        /// </summary>
        /// <param name="productSkus"></param>
        /// <returns></returns>
        ApiResult UpdateGradePrice(List<ProductSku> productSkus);

    }
}