using System.Collections.Generic;
using System.Linq;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    internal class ProductSkuRepository : RepositoryEfCore<ProductSku, long>, IProductSkuRepository {

        private const string ProdutSkuSql =
            @"SELECT   dbo.ZKShop_ProductSku.Bn, dbo.ZKShop_Product.Name, dbo.ZKShop_ProductSku.ProductId,dbo.ZKShop_ProductSku.MarketPrice,dbo.ZKShop_ProductSku.MaxPayPrice,dbo.ZKShop_ProductSku.MinPayCash,
                dbo.ZKShop_ProductSku.Id as ProductSkuId, dbo.ZKShop_ProductSku.Price, dbo.ZKShop_ProductSku.FenRunPrice,
                dbo.ZKShop_ProductSku.Weight,ZKShop_Product.IsFreeShipping, dbo.ZKShop_ProductSku.Stock, dbo.ZKShop_ProductSku.PropertyValueDesc,
                dbo.ZKShop_ProductSku.DisplayPrice, dbo.ZKShop_Product.ThumbnailUrl, dbo.ZKShop_Product.StoreId,dbo.ZKShop_Product.PriceStyleId,dbo.ZKShop_Product.DeliveryTemplateId
                FROM  dbo.ZKShop_ProductSku INNER JOIN
                dbo.ZKShop_Product ON dbo.ZKShop_ProductSku.ProductId = dbo.ZKShop_Product.Id";

        /// <summary>
        ///     添加商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        public bool AddStock(long productId, long productSkuId, int count) {
            var result = false;
            using (var transaction = RepositoryContext.BeginNativeDbTransaction()) {
                try {
                    var sql = $"update ZKShop_Product set Stock=Stock+{count} where Id={productId}";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    sql = $"update ZKShop_ProductSku set Stock=Stock+{count} where Id='{productSkuId}'";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    transaction.Commit();
                    result = true;
                } catch {
                    transaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the store width product sku.
        ///     根据商品SkuId获取商品Sku对象列表
        ///     获取上架商品
        /// </summary>
        /// <param name="productSkuIds">The product sku ids.</param>
        public IEnumerable<ProductSkuItem> GetProductSkuItemList(IEnumerable<long> productSkuIds) {
            var result = new List<ProductSkuItem>();
            var sql =
                $"{ProdutSkuSql}  where ZKShop_Product.ProductStatus=2  and  ZKShop_ProductSku.Stock>0 and ZKShop_ProductSku.Id  in ({productSkuIds.ToList().ToSqlString()})";
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var productSkuItem = new ProductSkuItem {
                        ProductSkuId = reader["ProductSkuId"].ConvertToLong(0),
                        ProductId = reader["ProductId"].ConvertToLong(0),
                        StoreId = reader["StoreId"].ConvertToLong(0),
                        Name = reader["name"].ToString(),
                        Stock = reader["Stock"].ConvertToLong(),
                        Bn = reader["Bn"].ToString(),
                        DisplayPrice = reader["DisplayPrice"].ToString(),
                        FenRunPrice = reader["FenRunPrice"].ToDecimal(),
                        MarketPrice = reader["MarketPrice"].ToDecimal(),
                        MaxPayPrice = reader["MaxPayPrice"].ToDecimal(),
                        MinPayCash = reader["MinPayCash"].ToDecimal(),
                        PropertyValueDesc = reader["PropertyValueDesc"].ToString(),
                        ThumbnailUrl = reader["ThumbnailUrl"].ToString(),
                        Price = reader["Price"].ToDecimal(),
                        PriceStyleId = reader["PriceStyleId"].ToGuid(),
                        Weight = reader["Weight"].ToDecimal(),
                        DeliveryTemplateId = reader["DeliveryTemplateId"].ToString()
                    };
                    result.Add(productSkuItem);
                }
            }

            return result;
        }

        /// <summary>
        ///     根据商品SkuId，获取店铺Id
        /// </summary>
        /// <param name="productSkuId"></param>
        public long GetStoreIdByProductSkuId(long productSkuId) {
            var sql =
                $"SELECT  ZKShop_Product.StoreId FROM ZKShop_Product INNER JOIN  ZKShop_ProductSku ON ZKShop_Product.Id = ZKShop_ProductSku.ProductId where ZKShop_ProductSku.Id={productSkuId}";
            var result = RepositoryContext.ExecuteScalar(sql).ToInt64();
            return result;
        }

        /// <summary>
        ///     减少商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        public bool ReduceStock(long productId, long productSkuId, int count) {
            var result = false;
            using (var transaction = RepositoryContext.BeginNativeDbTransaction()) {
                try {
                    var sql = $"update ZKShop_Product set Stock=Stock-{count} where Id={productId}";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    sql = $"update ZKShop_ProductSku set Stock=Stock-{count} where Id='{productSkuId}'";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    transaction.Commit();
                    result = true;
                } catch {
                    transaction.Rollback();
                    throw;
                }
            }

            return result;
        }

        /// <summary>
        ///     Gets the sku price.
        ///     获取所有商品的价格
        ///     在线状态的
        /// </summary>
        public IEnumerable<SkuPrice> GetSkuPrice(long productId = 0) {
            var sqlWhere = string.Empty;
            if (productId > 0) {
                sqlWhere = $" and ZKShop_Product.Id={productId} ";
            }

            var sql =
                $@"SELECT   dbo.ZKShop_ProductSku.Id, dbo.ZKShop_ProductSku.ProductId, dbo.ZKShop_ProductSku.Price, dbo.ZKShop_Product.PriceStyleId, dbo.ZKShop_Product.MinCashRate
            FROM dbo.ZKShop_ProductSku INNER JOIN dbo.ZKShop_Product ON dbo.ZKShop_ProductSku.ProductId = dbo.ZKShop_Product.Id Where ZKShop_Product.ProductStatus=2 {
                        sqlWhere
                    } order by id desc ";
            var result = new List<SkuPrice>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql)) {
                while (reader.Read()) {
                    var productSkuItem = new SkuPrice {
                        ProductSkuId = reader["Id"].ConvertToLong(0),
                        ProductId = reader["ProductId"].ConvertToLong(0),
                        Price = reader["Price"].ToDecimal(),
                        PriceStyleId = reader["PriceStyleId"].ToGuid(),
                        MinCashRate = reader["MinCashRate"].ToDecimal()
                    };
                    result.Add(productSkuItem);
                }
            }

            return result;
        }

        /// <summary>
        ///     Updates the sku price.
        ///     批量更新商品Sku价格，
        ///     自动处理，确保商品价格正确
        /// </summary>
        /// <param name="productSkus"></param>
        public void UpdateSkuPrice(IEnumerable<ProductSku> productSkus) {
            var sqlList = new List<string>();
            foreach (var item in productSkus) {
                var sql =
                    $"update ZKShop_ProductSku set DisplayPrice='{item.DisplayPrice}',MaxPayPrice='{item.MaxPayPrice}',MinPayCash='{item.MinPayCash}' where id={item.Id}";
                sqlList.Add(sql);
            }

            // 批量更新数据库
            RepositoryContext.ExecuteSqlList(sqlList);
        }

        public ProductSkuRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}