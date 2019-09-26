using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.Domain.Enums {

    /// <summary>
    ///     商城行业模型
    /// </summary>
    [ClassProperty(Name = "商城行业模型")]
    public enum IndustrySytle {

        /// <summary>
        ///     通用商品模型，百货商品，比如服装、眼睛、箱包
        /// </summary>
        [Display(Name = "百货行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699001")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Common = 1,

        /// <summary>
        ///     旅游行业
        /// </summary>
        [Display(Name = "旅游行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699002")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Trip = 2,

        /// <summary>
        ///     酒店行业
        /// </summary>
        [Display(Name = "酒店行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699003")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Hotel = 3,

        /// <summary>
        ///     通用商品模型，百货商品，比如服装、眼睛、箱包
        /// </summary>
        [Display(Name = "餐饮行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699004")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Food = 4,

        /// <summary>
        ///     保险行业
        /// </summary>
        [Display(Name = "保险行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699005")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Insurance = 5,

        /// <summary>
        ///     虚拟商品
        /// </summary>
        [Display(Name = "保险行业")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699006")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Virtual = 6,

        /// <summary>
        ///     P2P网贷
        /// </summary>
        [Display(Name = "P2P网贷")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699007")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        P2P = 7,

        /// <summary>
        ///     众筹
        /// </summary>
        [Display(Name = "众筹")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D699008")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Raise = 8,

        /// <summary>
        ///     一元夺宝
        /// </summary>
        [Display(Name = "一元夺宝")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D6990100")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Indiana = 100,

        /// <summary>
        ///     拼图
        /// </summary>
        [Display(Name = "拼图")]
        [Field(IsDefault = true, GuidId = "B0000000-1478-49BD-BFC7-E73A5D6990101")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        SpellGroup = 101
    }
}