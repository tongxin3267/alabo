using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     充值方式
    /// </summary>
    [ClassProperty(Name = "充值方式")]
    public enum RchargeStyle
    {
        /// <summary>
        ///     线上充值
        /// </summary>
        [Display(Name = "线上充值")] [LabelCssClass(BadgeColorCalss.Danger)]
        Onlinerecharge = 0,

        /// <summary>
        ///     线下充值
        /// </summary>
        [Display(Name = "线下充值")] [LabelCssClass(BadgeColorCalss.Danger)]
        Offlinerecharge = 1,

        /// <summary>
        ///     微信充值
        /// </summary>
        [Display(Name = "微信充值")] [LabelCssClass(BadgeColorCalss.Danger)]
        Microchannelrecharge = 2,

        /// <summary>
        ///     支付宝充值
        /// </summary>
        [Display(Name = "支付宝充值")] [LabelCssClass(BadgeColorCalss.Danger)]
        Alipayrecharge = 3
    }
}