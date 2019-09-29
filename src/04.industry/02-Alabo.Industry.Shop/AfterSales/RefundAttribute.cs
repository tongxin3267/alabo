using System;
using Alabo.Industry.Shop.Orders.Domain.Enums;

namespace Alabo.Industry.Shop.AfterSales
{
    /// <summary>
    ///     订单操作方式
    ///     更加状态自动判断订单是否可以操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RefundTypeAttribute : Attribute
    {
        /// <summary>
        ///     操作类型 用于前端判断
        /// </summary>
        public OrderStatus Type { get; set; }
    }
}