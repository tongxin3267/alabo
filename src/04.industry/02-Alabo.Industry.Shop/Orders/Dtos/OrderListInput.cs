using Alabo.Domains.Query.Dto;
using Alabo.Industry.Shop.Orders.Domain.Enums;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     订单
    /// </summary>
    public class OrderListInput : ApiInputDto
    {
        /// <summary>
        ///     订单状态
        /// </summary>
        public OrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     获取订单类型
        ///     UserOrderList = 1, // 会员订单
        ///     StoreOrderList = 2, // 店铺订单
        ///     AdminOrderList = 3 // 平台订单
        /// </summary>

        public OrderType? OrderType { get; set; } = Domain.Enums.OrderType.Normal;

        /// <summary>
        ///     获取会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     发货用户Id
        /// </summary>
        public long DeliverUserId { get; set; }

        /// <summary>
        ///     当前页 private
        /// </summary>
        public long PageIndex { get; set; }

        /// <summary>
        ///     每页记录数 private
        /// </summary>

        public long PageSize { get; set; }
    }
}