using System.Collections.Generic;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Activitys.Modules.ProductNumberLimit.Dtos;
using Alabo.Industry.Shop.Orders.Domain.Entities;

namespace Alabo.Industry.Shop.Orders.Domain.Repositories
{

    public interface IOrderProductRepository : IRepository<OrderProduct, long>
    {

        /// <summary>
        /// GetUserProductCount
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        List<UserProductCount> GetUserProductCount(long UserId, List<long> productIds);

        /// <summary>
        /// GetProductCount
        /// </summary>
        /// <param name="productIds"></param>
        /// <returns></returns>
        List<ProductCount> GetProductCount(List<long> productIds);
    }
}