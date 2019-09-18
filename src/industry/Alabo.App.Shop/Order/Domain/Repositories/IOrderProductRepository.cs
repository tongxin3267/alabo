using System.Collections.Generic;
using Alabo.App.Shop.Activitys.Modules.ProductNumberLimit.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories
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