using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Products.Domain.Repositories
{
    public interface IProductSkuRepository : IRepository<ProductSku, long>
    {
        /// <summary>
        ///     根据商品SkuId，获取店铺Id
        /// </summary>
        /// <param name="productSkuId"></param>
        ObjectId GetStoreIdByProductSkuId(long productSkuId);

        /// <summary>
        ///     添加商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        bool AddStock(long productId, long productSkuId, int count);

        /// <summary>
        ///     减少商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        bool ReduceStock(long productId, long productSkuId, int count);

        /// <summary>
        ///     Gets the store width product sku.
        ///     根据商品SkuId获取商品Sku对象列表
        /// </summary>
        /// <param name="productSkuIds">The product sku ids.</param>
        IEnumerable<ProductSkuItem> GetProductSkuItemList(IEnumerable<long> productSkuIds);

        /// <summary>
        ///     Gets the sku price.
        ///     获取所有商品的价格
        ///     在线状态的
        /// </summary>
        IEnumerable<SkuPrice> GetSkuPrice(long productId = 0);

        /// <summary>
        ///     Updates the sku price.
        ///     批量更新商品Sku价格，
        ///     自动处理，确保商品价格正确
        /// </summary>
        void UpdateSkuPrice(IEnumerable<ProductSku> productSkus);
    }
}