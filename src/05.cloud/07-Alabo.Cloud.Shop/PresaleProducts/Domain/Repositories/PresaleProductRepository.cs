using System;
using System.Collections.Generic;
using System.Data;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Entities;
using Alabo.Cloud.Shop.PresaleProducts.Domain.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Industry.Shop.Products.Domain.Enums;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Repositories {

    public class PresaleProductRepository : RepositoryEfCore<PresaleProduct, ObjectId>, IPresaleProductRepository {

        public PresaleProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public List<PresaleProductItem> GetPresaleProducts(PresaleProductApiInput input, out long count) {
            if (input.PageIndex < 0) {
                throw new ArgumentNullException("pageIndex", "pageindex has to be greater than 1");
            }
            if (input.PageSize > 100) {
                input.PageSize = 100;
            }
            if (input.StartPrice > input.EndPrice) {
                throw new ArgumentNullException("price", "开始区间不大于结束区间");
            }

            //where
            var sqlWhere = string.Empty;
            if (input.PriceStyleId.HasValue) {
                sqlWhere += $" AND presale.PriceStyleId ='{input.PriceStyleId}'";
            }
            if (!string.IsNullOrEmpty(input.ProductName)) {
                sqlWhere += $" AND product.Name Like '%{input.ProductName}%'";
            }
            if (!string.IsNullOrEmpty(input.CategoryId)) {
                sqlWhere += $" AND product.CategoryId='{input.CategoryId}'";
            }
            if (input.StartPrice > 0 && input.EndPrice > 0) {
                sqlWhere += $" AND presale.VirtualPrice>={input.StartPrice} AND presale.VirtualPrice<={input.EndPrice}";
            }
            //status
            var status = (int)ProductStatus.Online;
            sqlWhere += $" AND product.ProductStatus= {status}  AND product.StoreId>0 AND presale.Status={status}";

            //count
            var sqlCount = $@"SELECT COUNT(presale.Id) [Count]
                            FROM Shop_PresaleProduct AS presale
                            INNER JOIN Shop_Product AS product ON presale.ProductId = product.Id AND presale.PriceStyleId=product.PriceStyleId
                            INNER JOIN Shop_Category AS category ON category.Id = product.CategoryId
                            LEFT JOIN Shop_ProductSku AS sku ON sku.Id = presale.SkuId
                            where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            //datas
            var sql = $@"SELECT TOP {input.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY presale.CreateTime) AS RowNumber,
                                presale.Id,
                                presale.ProductId,
                                product.Name AS ProductName,
                                product.ThumbnailUrl,
                                sku.PropertyValueDesc AS SkuName,
                                sku.CostPrice,
                                category.Name AS ProductTypeName,
                                presale.VirtualPrice,
                                presale.Stock,
                                presale.QuantitySold,
                                presale.Status,
                                presale.Sort
                        FROM Shop_PresaleProduct AS presale
                        INNER JOIN Shop_Product AS product ON presale.ProductId = product.Id AND presale.PriceStyleId=product.PriceStyleId
                        INNER JOIN Shop_Category AS category ON category.Id = product.CategoryId
                        LEFT JOIN Shop_ProductSku AS sku ON sku.Id = presale.SkuId
                        where 1=1 {sqlWhere}
                    ) as A
                   WHERE RowNumber > {input.PageSize}*({input.PageIndex}-1) ";
            var result = new List<PresaleProductItem>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadProduct(dr));
                }
            }
            return result;
        }

        private PresaleProductItem ReadProduct(IDataReader reader) {
            var product = new PresaleProductItem {
                Id = reader["Id"].ConvertToLong(0),
                ProductId = reader["ProductId"].ConvertToLong(0),
                ProductName = reader["ProductName"].ToString(),
                ThumbnailUrl = reader["ThumbnailUrl"].ToString(),
                SkuName = reader["SkuName"].ToString(),
                CostPrice = reader["CostPrice"].ToDecimal(),
                ProductTypeName = reader["ProductTypeName"].ToString(),
                VirtualPrice = reader["VirtualPrice"].ToDecimal(),
                Stock = reader["Stock"].ConvertToInt(0),
                QuantitySold = reader["QuantitySold"].ConvertToInt(0),
                Status = reader["Status"].ConvertToInt(0),
                Sort = reader["Sort"].ConvertToInt(0)
            };
            return product;
        }
    }
}