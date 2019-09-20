using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     支付平台
    /// </summary>
    [ClassProperty(Name = "支付平台")]
    public enum PayPlatform
    {
        /// <summary>
        ///     PC
        /// </summary>
        [Display(Name = "PC")]
        [Field(IsDefault = true, GuidId = "E0000000-1715-21CD-89GF-F00000000000")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Pc = 0,

        /// <summary>
        ///     手机浏览器
        /// </summary>
        [Display(Name = "手机浏览器")]
        [Field(IsDefault = true, GuidId = "E0000000-1715-21CD-89GF-F00000000001")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        MobileBrowser,

        /// <summary>
        ///     微信公众号
        /// </summary>
        [Display(Name = "微信公众号")]
        [Field(IsDefault = true, GuidId = "E0000000-1715-21CD-89GF-F00000000002")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        WeChat,

        /// <summary>
        ///     手机App
        /// </summary>
        [Display(Name = "手机App")]
        [Field(IsDefault = true, GuidId = "E0000000-1715-21CD-89GF-F00000000003")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        MobileApp
    }
}