﻿using System.Collections.Generic;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Industry.Shop.Activitys.Modules.ProductNumberLimit.Dtos;
using Alabo.Industry.Shop.Orders.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Domain.Repositories
{
    /// <summary>
    ///     OrderProductRepository
    /// </summary>
    public class OrderProductRepository : RepositoryEfCore<OrderProduct, long>, IOrderProductRepository
    {
        /// <summary>
        ///     constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public OrderProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     GetUserProductCount
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="productIds"></param>
        public List<UserProductCount> GetUserProductCount(long userId, List<long> productIds)
        {
            var sql = $@"select Count,userId,productId from Shop_OrderProduct
                        inner join Shop_Order
                        on Shop_Order.Id=Shop_OrderProduct.OrderId
                        where ProductId in ({productIds.ToSqlString()}) and userid={userId} and (OrderStatus!=200 and OrderStatus!=1)";
            var result = new List<UserProductCount>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                while (reader.Read())
                {
                    var userProductCount = new UserProductCount
                    {
                        UserId = reader["userId"].ConvertToLong(),
                        Count = reader["Count"].ConvertToLong(),
                        ProductId = reader["ProductId"].ConvertToLong()
                    };
                    result.Add(userProductCount);
                }
            }

            return result;
        }

        /// <summary>
        ///     GetProductCount
        /// </summary>
        /// <param name="productIds"></param>
        public List<ProductCount> GetProductCount(List<long> productIds)
        {
            var sql = $@"select Count,userId,productId from Shop_OrderProduct
                        inner join Shop_Order
                        on Shop_Order.Id=Shop_OrderProduct.OrderId
                        where ProductId in ({productIds.ToSqlString()}) and (OrderStatus!=200 and OrderStatus!=1)";
            var result = new List<ProductCount>();
            using (var reader = RepositoryContext.ExecuteDataReader(sql))
            {
                while (reader.Read())
                {
                    var productCount = new ProductCount
                    {
                        Count = reader["Count"].ConvertToLong(),
                        ProductId = reader["ProductId"].ConvertToLong()
                    };
                    result.Add(productCount);
                }
            }

            return result;
        }
    }
}