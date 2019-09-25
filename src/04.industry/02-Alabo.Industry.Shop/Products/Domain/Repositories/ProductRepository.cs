using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Shop.Product.Domain.Repositories {

    internal class ProductRepository : RepositoryEfCore<Entities.Product, long>, IProductRepository {

        public List<ProductItem> GetProductItems(ProductApiInput input, out long count) {
            if (input.PageIndex < 0)
            {
                input.PageIndex = 1;
            }

            if (input.PageSize > 100) {
                input.PageSize = 100;
            }

            #region 标签 分类查询

            var TcSql = string.Empty;

            if (!string.IsNullOrEmpty(input.ClassIds)) {
                TcSql =
                    $"{TcSql} AND Type='{typeof(ProductClassRelation).FullName}'  AND Id in ({input.ClassIds})  or FatherId in ({input.ClassIds}) ";
            }

            if (!string.IsNullOrEmpty(input.TagIds)) {
                TcSql =
                    $"{TcSql} AND Type='{typeof(ProductTagRelation).FullName}'  AND Id in ({input.TagIds})  or FatherId in ({input.TagIds}) ";
            }

            if (!string.IsNullOrEmpty(TcSql)) {
                TcSql = $@"SELECT DISTINCT EntityId FROM Basic_RelationIndex  WHERE RelationId IN(
                        SELECT Id FROM Basic_Relation WHERE 1=1 {TcSql} )";
            }

            #endregion 标签 分类查询

            var sqlWhere = string.Empty;
            if (!string.IsNullOrEmpty(input.Keyword)) {
                sqlWhere = $"{sqlWhere} AND Name Like '%{input.Keyword}%'";
            }
            if (input.MinPrice.HasValue) {
                sqlWhere = $"{sqlWhere} AND Price>{input.MinPrice}";
            }
            if (input.MaxPrice.HasValue) {
                sqlWhere = $"{sqlWhere} AND Price<{input.MaxPrice}";
            }
            if (input.PriceStyleId.HasValue) {
                sqlWhere = $"{sqlWhere} AND PriceStyleId ='{input.PriceStyleId}'";
            }
            if (input.BrandId.HasValue) {
                sqlWhere = $"{sqlWhere} AND  BrandId ='{input.BrandId}'";
            }
            if (!TcSql.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Id in ({TcSql})";
            }
            if (!input.ProductIds.IsNullOrEmpty()) {
                sqlWhere = $"{sqlWhere} AND Id in ({input.ProductIds})";
            }
            //商品状态过滤  商品店铺过滤
            sqlWhere = $"{sqlWhere} AND ProductStatus= {(int)ProductStatus.Online}";
            sqlWhere = $"{sqlWhere} AND StoreId>0";

            // 库存
            sqlWhere = $"{sqlWhere} AND Stock <= {input.Stock} ";

            var priceStyleId = Guid.Empty;
         
            // 商城模式状态不正常，获取不存在的，商品不显示
            if (input.PriceStyles != null && input.PriceStyles.Count > 0 && !input.PriceStyleId.IsGuidNullOrEmpty()) {
                input.PriceStyles = input.PriceStyles.Where(r => input.PriceStyleId != null && r.Id == (Guid)input.PriceStyleId).ToList();
                var priceStyleIdList = input.PriceStyles.Select(r => r.Id).ToList();
                sqlWhere = $"{sqlWhere} AND PriceStyleId ='{input.PriceStyleId}' ";
            }

            var sqlCount = $"SELECT COUNT(Id) [Count] FROM Shop_Product where 1=1 {sqlWhere}";
            count = RepositoryContext.ExecuteScalar(sqlCount)?.ConvertToLong() ?? 0;

            //排序处理  desc 降序
            var sort = string.Empty;
            if (input.SortOrder != ProductSortOrder.Defualt) {
                sort = input.SortOrder.ToString();
            } else {
                sort = "Id";
            }

            if (input.OrderType == 0) {
                sort = $"{sort} desc";
            }
            // 如果有传入指定数量，不分页，输出具体的数量
            if (input.Count > 0) {
                input.PageSize = input.Count;
                input.PageIndex = 1;
            }

            var sql = $@"SELECT TOP {input.PageSize} * FROM (
                        SELECT  ROW_NUMBER() OVER (ORDER BY {sort}) AS RowNumber,* FROM Shop_Product  where 1=1 {
                    sqlWhere
                }
                               ) as A
                        WHERE RowNumber > {input.PageSize}*({input.PageIndex}-1) ";
            var result = new List<ProductItem>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadProduct(dr));
                }
            }

            return result;
        }

        private ProductItem ReadProduct(IDataReader reader) {
            var product = new ProductItem {
                Id = reader["Id"].ConvertToLong(0),
                Name = reader["Name"].ToString(),
                Bn = reader["Bn"].ToString(),
                MarketPrice = reader["MarketPrice"].ToDecimal(),
                Price = reader["Price"].ToDecimal(),
                DisplayPrice = reader["DisplayPrice"].ToString(),
                ThumbnailUrl = reader["ThumbnailUrl"].ToString(),
                SoldCount = reader["SoldCount"].ConvertToLong(0),
                PriceStyleId = reader["PriceStyleId"].ToGuid(),
                ViewCount = reader["ViewCount"].ConvertToLong(0),
                Stock= reader["Stock"].ConvertToLong(0)
            };
            return product;
        }

        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}