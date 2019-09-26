using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Offline.Product.Domain.Enums
{
    [ClassProperty(Name = "商品套餐类型")]
    public enum ProductTypeEnum
    {
        /// <summary>
        /// 单品
        /// </summary>
        [Display(Name = "单品")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Single = 1,

        /// <summary>
        /// 套餐
        /// </summary>
        [Display(Name = "套餐")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Package = 2
    }
}
