using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     卡券类型
    /// </summary>
    [ClassProperty(Name = "卡券类型")]
    public enum CardCouponsType
    {
        /// <summary>
        ///     折扣券
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "折扣券")]
        Discount = 1,

        /// <summary>
        ///     兑换券
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "兑换券")]
        Exchange = 2,

        /// <summary>
        ///     优惠券
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "优惠券")]
        Preferential = 3,

        /// <summary>
        ///     代金券
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "代金券")]
        Kims = 4,

        /// <summary>
        ///     红包
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "红包")]
        RedPacket = 5,

        /// <summary>
        ///     免邮卡
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "免邮卡")]
        FreeShipping = 6
    }
}