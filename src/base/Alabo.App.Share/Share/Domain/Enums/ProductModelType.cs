using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Share.Domain.Enums {

    /// <summary>
    /// 商品范围
    /// </summary>
    [ClassProperty(Name = "商品范围")]
    public enum ProductModelType {

        /// <summary>
        /// 全商品模式
        /// </summary>
        [Display(Name = "所有商品")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        All = 0,

        /// <summary>
        /// 产品线模式
        /// </summary>
        [Display(Name = "按所属商品线选择")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        ProductLine = 1,

        /// <summary>
        /// 商城模式
        /// </summary>
        [Display(Name = "按所属商城选择")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        ShoppingMall = 2,

        ///// <summary>
        ///// 多商品
        ///// </summary>
        //[Display(Name = "多商品")]
        //[LabelCssClass(BadgeColorCalss.Warning )]
        //UserDefined = 3

        ///// <summary>
        ///// 单商品
        ///// </summary>
        //[Display(Name = "单商品")]
        //[LabelCssClass(BadgeColorCalss.Warning )]
        //UserDefined = 4
    }
}