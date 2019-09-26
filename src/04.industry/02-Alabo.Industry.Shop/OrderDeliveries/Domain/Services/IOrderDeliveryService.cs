using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;
using Alabo.Industry.Shop.Orders.ViewModels;

namespace Alabo.Industry.Shop.OrderDeliveries.Domain.Services
{
    /// <summary>
    ///     发货记录
    /// </summary>
    public interface IOrderDeliveryService : IService<OrderDelivery, long>
    {
        /// <summary>
        ///     获取所有的发货记录
        /// </summary>
        /// <param name="orderId"></param>
        List<OrderDelivery> GetOrderDeliveries(long orderId);

        /// <summary>
        ///     发货记录
        /// </summary>
        /// <param name="query"></param>
        PagedList<ViewOrderDeliveryList> GetPageList(object query);

        /// <summary>
        ///     Gets the view.
        /// </summary>
        /// <param name="id">Id标识</param>
        ViewOrderDeliveryList GetViewOrderDeliveryEdit(long id);

        /// <summary>
        ///     Gets the order deliver user identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        long GetOrderDeliverUserId(long orderId);
    }
}