﻿using Alabo.Domains.Query.Dto;
using Alabo.Industry.Shop.Orders.Domain.Enums;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    public class AdminOrderInput : PagedInputDto
    {
        /// <summary>
        ///     订单状态
        /// </summary>
        public OrderStatus? OrderStatus { get; set; }

        /// <summary>
        ///     获取订单类型
        ///     UserOrderList = 1, // 会员订单
        ///     StoreOrderList = 2, // 店铺订单
        ///     AdminOrderList = 3 // 平台订单
        /// </summary>

        public OrderType? Type { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is planform.
        ///     是否为平台商品
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is planform; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlanform { get; set; } = false;
    }
}