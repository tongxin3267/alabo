using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     收银台配置类型
    /// </summary>
    [ClassProperty(Name = "收银台配置类型")]
    public enum CheckoutType
    {
        /// <summary>
        ///     商城订单
        ///     订单表在ZkShop_Order表中
        /// </summary>
        [Display(Name = "订单")]
        [LabelCssClass("m-badge--primary")]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D679000")]
        Order = 1,

        /// <summary>
        ///     充值
        /// </summary>
        [Display(Name = "充值")]
        [LabelCssClass("m-badge--warning")]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D679002")]
        Recharge = 2,

        /// <summary>
        ///     等级购买
        /// </summary>
        [Display(Name = "等级购买")]
        [LabelCssClass("m-badge--success")]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D679003")]
        GradeBuy = 3,

        /// <summary>
        ///     餐饮
        /// </summary>
        [Display(Name = "餐饮")]
        [LabelCssClass("m-badge--info")]
        [Field(IsDefault = true, GuidId = "E97CCD1E-1478-49BD-BFC7-E73A5D679004")]
        Catering = 4,

        /// <summary>
        ///     定制套餐
        /// </summary>
        [Display(Name = "定制套餐")]
        [Field(IsDefault = true, GuidId = "2F785BE2-28F5-4207-95AE-234709125B08")]
        [LabelCssClass("m-badge--info")]
        Offer = 5,

        /// <summary>
        ///     线下商城
        /// </summary>
        Offline = 6,

        /// <summary>
        ///     自定义
        /// </summary>
        [Display(Name = "自定义")] [LabelCssClass("m-badge--warning")]
        Customer = 100
    }
}