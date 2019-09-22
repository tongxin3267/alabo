using System.Collections.Generic;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public interface IOrderActionRepository : IRepository<OrderAction, long> {

        /// <summary>
        ///     Deletes the cart buy order.
        ///     订单生成成功以后，删除购物车数据
        /// </summary>
        void DeleteCartBuyOrder();

        /// <summary>
        ///     获取订单列表导出表格用的数据
        /// </summary>
        List<OrderToExcel> GetOrderListToExcel();
    }
}