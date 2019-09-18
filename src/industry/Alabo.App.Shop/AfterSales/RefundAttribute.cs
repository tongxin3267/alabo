using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Shop.Order.Domain.Enums;

namespace Alabo.App.Shop.AfterSales
{
    /// <summary>
    ///     订单操作方式
    ///     更加状态自动判断订单是否可以操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class RefundTypeAttribute : Attribute
    {

        /// <summary>
        /// 操作类型 用于前端判断
        /// </summary>
        public OrderStatus Type { get; set; }
    }
}
