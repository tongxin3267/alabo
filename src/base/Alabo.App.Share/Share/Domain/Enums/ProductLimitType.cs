using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Share.Domain.Enums {

    /// <summary>
    /// 产品限制方式
    /// </summary>
    [ClassProperty(Name = "产品限制方式")]
    public enum ProductLimitType {

        /// <summary>
        /// 产品线范围内
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "选择商品范围内")]
        Allow = 1,

        /// <summary>
        /// 非产品线内的
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "选择商品范围外")]
        Refuse = 2
    }
}