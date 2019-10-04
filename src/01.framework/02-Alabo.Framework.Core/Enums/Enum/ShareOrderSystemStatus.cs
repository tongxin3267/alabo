using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     系统订单状态
    /// </summary>
    [ClassProperty(Name = "系统订单状态")]
    public enum ShareOrderSystemStatus {

        /// <summary>
        ///     待处理
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "待处理")]
        [Field(Icon = "la la-lock")]
        Pending = 1,

        /// <summary>
        ///     会员已统计
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "会员已统计")]
        [Field(Icon = "la la-lock")]
        UserHandled = 2,

        /// <summary>
        ///     订单已统计
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "订单已统计")]
        [Field(Icon = "la la-lock")]
        OrderHandled = 3,

        /// <summary>
        ///     发生错误
        /// </summary>
        Error = 10,

        /// <summary>
        ///     已取消
        /// </summary>
        Canceld = 11,

        /// <summary>
        ///     订单等待完成
        /// </summary>
        OrderPendingSucess = 20
    }
}