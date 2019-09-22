using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Enums {

    /// <summary>
    ///     订单类型
    /// </summary>
    [ClassProperty(Name = "订单类型")]
    public enum OrderType {

        /// <summary>
        ///     一般订单
        /// </summary>
        [Display(Name = "一般订单")]
        Normal = 1,

        /// <summary>
        ///     虚拟订单
        /// </summary>
        [Display(Name = "虚拟订单")]
        VirtualOrder = 2,

        /// <summary>
        ///     批发订单
        /// </summary>
        [Display(Name = "批发订单")]
        BatchSale = 3,

        /// <summary>
        ///     预定类型的订单
        /// </summary>
        [Display(Name = "预约订单")]
        Booking = 4
    }
}