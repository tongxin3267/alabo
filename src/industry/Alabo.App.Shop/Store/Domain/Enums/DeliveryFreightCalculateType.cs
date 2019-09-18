using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Store.Domain.Enums {

    /// <summary>
    ///     运费计算方式
    /// </summary>
    [ClassProperty(Name = "运费计算方式")]
    public enum DeliveryFreightCalculateType {

        /// <summary>
        ///     按商品重量计算运费
        /// </summary>
        [Display(Name = "按商品重量计算运费")]
        [LabelCssClass("text-default")]
        ByWeight = 0,

        /// <summary>
        ///     按宝贝件数计算运费
        /// </summary>
        [Display(Name = "按宝贝件数计算运费 ")]
        [LabelCssClass("text-default")]
        ByCount = 1,

        /// <summary>
        ///     按商品体积计算运费
        /// </summary>
        [Display(Name = "按商品体积计算运费")]
        [LabelCssClass("text-default")]
        BySize = 2
    }
}