using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.Domain.Enums {

    /// <summary>
    ///     商城模式，价格模式
    /// </summary>
    [ClassProperty(Name = "商城与价格模式")]
    public enum PriceStyle {

        /// <summary>
        ///     Mark标识表示商城首页URL
        ///     商品全部由现金购买
        /// </summary>
        [Display(Name = "现金商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699000", Mark = "Cash")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        CashProduct = 100, //0改100

        /// <summary>
        ///     商品全部由积分购买
        /// </summary>
        [Display(Name = "积分商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699111", Mark = "Point")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        PointProduct = 1,

        /// <summary>
        ///     商品全部由虚拟币购买
        /// </summary>
        [Display(Name = "虚拟币商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699011", Mark = "Virtual")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        VirtualProduct = 2,

        /// <summary>
        ///     现金+积分
        /// </summary>
        [Display(Name = "现金+积分商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699012", Mark = "CashAndPoint")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        CashAndPoint = 101,

        /// <summary>
        ///     现金+虚拟币
        /// </summary>
        [Display(Name = " 现金+虚拟币商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699013", Mark = "CashAndVirtual")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        CashAndVirtual = 102,

        /// <summary>
        ///     现金+授信
        /// </summary>
        [Display(Name = "优惠商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699014", Mark = "CashAndCredit")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        CashAndCredit = 103,

        /// <summary>
        ///    消费额(货币类型:提现)
        /// </summary>
        [Display(Name = "消费额")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699017", Mark = "ShopAmount")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        ShopAmount = 104,

        /// <summary>
        ///     商品全部授信购买
        /// </summary>
        [Display(Name = "授信商城")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699015", Mark = "Credit")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        CreditProduct = 3,

        /// <summary>
        ///     自定义
        /// </summary>
        [Display(Name = "自定义")]
        [Field(IsDefault = false, GuidId = "E0000000-1478-49BD-BFC7-E73A5D699016")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Customer = 100000
    }
}