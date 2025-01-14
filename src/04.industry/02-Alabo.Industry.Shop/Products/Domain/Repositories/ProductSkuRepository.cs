﻿using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Dtos;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Products.Domain.Repositories
{
    internal class ProductSkuRepository : RepositoryEfCore<ProductSku, long>, IProductSkuRepository
    {
        private const string ProdutSkuSql =
            @"SELECT   dbo.Shop_ProductSku.Bn, dbo.Shop_Product.Name, dbo.Shop_ProductSku.ProductId,dbo.Shop_ProductSku.MarketPrice,dbo.Shop_ProductSku.MaxPayPrice,dbo.Shop_ProductSku.MinPayCash,
                dbo.Shop_ProductSku.Id as ProductSkuId, dbo.Shop_ProductSku.Price, dbo.Shop_ProductSku.FenRunPrice,
                dbo.Shop_ProductSku.Weight,Shop_Product.IsFreeShipping, dbo.Shop_ProductSku.Stock, dbo.Shop_ProductSku.PropertyValueDesc,
                dbo.Shop_ProductSku.DisplayPrice, dbo.Shop_Product.ThumbnailUrl, dbo.Shop_Product.StoreId,dbo.Shop_Product.PriceStyleId,dbo.Shop_Product.DeliveryTemplateId
                FROM  dbo.Shop_ProductSku INNER JOIN
                dbo.Shop_Product ON dbo.Shop_ProductSku.ProductId = dbo.Shop_Product.Id";

        public ProductSkuRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     添加商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        public bool AddStock(long productId, long productSkuId, int count)
        {
            var result = false;
            using (var transaction = RepositoryContext.BeginNativeDbTransaction())
            {
                try
                {
                    var sql = $"update Shop_Product set Stock=Stock+{count} where Id={productId}";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    sql = $"update Shop_ProductSku set Stock=Stock+{count} where Id='{productSkuId}'";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    transaction.Commit();
                    result = true;
                }
                catch
                {
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
        public IEnumerable<ProductSkuItem> GetProductSkuItemList(IEnumerable<long> productSkuIds)
        {
            var result = new List<ProductSkuItem>();
            var sql =
                $"{ProdutSkuSql}  where Shop_Product.ProductStatus=2  and  Shop_ProductSku.Stock>0 and Shop_ProductSku.Id  in ({productSkuIds.ToList().ToSqlString()})";
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                while (reader.Read())
                {
                    var productSkuItem = new ProductSkuItem
                    {
                        ProductSkuId = reader["ProductSkuId"].ConvertToLong(0),
                        ProductId = reader["ProductId"].ConvertToLong(0),
                        StoreId = reader["StoreId"].ToObjectId(),
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
        public ObjectId GetStoreIdByProductSkuId(long productSkuId)
        {
            var sql =
                $"SELECT  Shop_Product.StoreId FROM Shop_Product INNER JOIN  Shop_ProductSku ON Shop_Product.Id = Shop_ProductSku.ProductId where Shop_ProductSku.Id={productSkuId}";
            var result = RepositoryContext.ExecuteScalar(sql);
            return result.ToObjectId();
        }

        /// <summary>
        ///     减少商品库存
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productSkuId"></param>
        /// <param name="count"></param>
        public bool ReduceStock(long productId, long productSkuId, int count)
        {
            var result = false;
            using (var transaction = RepositoryContext.BeginNativeDbTransaction())
            {
                try
                {
                    var sql = $"update Shop_Product set Stock=Stock-{count} where Id={productId}";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    sql = $"update Shop_ProductSku set Stock=Stock-{count} where Id='{productSkuId}'";
                    RepositoryContext.ExecuteNonQuery(transaction, sql);
                    transaction.Commit();
                    result = true;
                }
                catch
                {
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
        public IEnumerable<SkuPrice> GetSkuPrice(long productId = 0)
        {
            var sqlWhere = string.Empty;
            if (productId > 0) {
                sqlWhere = $" and Shop_Product.Id={productId} ";
            }

            var sql =
                $@"SELECT   dbo.Shop_ProductSku.Id, dbo.Shop_ProductSku.ProductId, dbo.Shop_ProductSku.Price, dbo.Shop_Product.PriceStyleId, dbo.Shop_Product.MinCashRate
            FROM dbo.Shop_ProductSku INNER JOIN dbo.Shop_Product ON dbo.Shop_ProductSku.ProductId = dbo.Shop_Product.Id Where Shop_Product.ProductStatus=2 {
                        sqlWhere
                    } order by id desc ";
            var result = new List<SkuPrice>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                while (reader.Read())
                {
                    var productSkuItem = new SkuPrice
                    {
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
        public void UpdateSkuPrice(IEnumerable<ProductSku> productSkus)
        {
            var sqlList = new List<string>();
            foreach (var item in productSkus)
            {
                var sql =
                    $"update Shop_ProductSku set DisplayPrice='{item.DisplayPrice}',MaxPayPrice='{item.MaxPayPrice}',MinPayCash='{item.MinPayCash}' where id={item.Id}";
                sqlList.Add(sql);
            }

            // 批量更新数据库
            RepositoryContext.ExecuteSqlList(sqlList);
        }
    }
}