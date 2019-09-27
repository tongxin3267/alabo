using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.Industry.Shop.Deliveries.Dtos;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services
{
    /// <summary>
    ///     Interface IStoreService
    /// </summary>
    public interface IShopStoreService : IService
    {
        /// <summary>
        ///     Counts the express fee.
        ///     根据客户选择的物流方式，商品重量，计算快递费用
        /// </summary>
        /// <param name="storeId">The stroe identifier.</param>
        /// <param name="templateId">The express identifier.</param>
        /// <param name="userAddress">用户地址</param>
        /// <param name="weight">The weight.</param>
        Tuple<ServiceResult, decimal> CountExpressFee(ObjectId storeId, ObjectId templateId, UserAddress userAddress, decimal weight);

        /// <summary>
        ///     Gets the store item list from cache.
        ///     从缓存中，读取StoreItem对象
        ///     如果店铺数据多的情况下，该方法需要优化
        /// </summary>
        IEnumerable<StoreItem> GetStoreItemListFromCache();

        /// <summary>
        ///     根据店铺Id列表，和商品skuId列表，输出店铺商品显示对象
        ///     用户购物车、客户下单
        ///     店铺数量多的情况下，改方法需要优化，店铺数量少的情况下，将店铺数据插入到了缓存了，所有性能较快
        ///     Gets the store product sku.
        /// </summary>
        /// <param name="storeProductSkuDtos">The store product sku dtos.</param>
        Tuple<ServiceResult, StoreProductSku> GetStoreProductSku(StoreProductSkuDtos storeProductSkuDtos);

        /// <summary>
        /// Counts the express fee.
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userAddress"></param>
        /// <param name="productSkuItems"></param>
        /// <returns></returns>
        Tuple<ServiceResult, decimal> CountExpressFee(ObjectId storeId, UserAddress userAddress, IList<ProductSkuItem> productSkuItems);
    }
}