using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.Domain.Enums
{
    /// <summary>
    ///     商品状态
    /// </summary>
    [ClassProperty(Name = "商品状态")]
    public enum ProductStatus
    {
        /// <summary>
        ///     审核中
        /// </summary>
        [Display(Name = "审核中")] [LabelCssClass(BadgeColorCalss.Metal)]
        Auditing = 1,

        /// <summary>
        ///     已上架
        /// </summary>
        [Display(Name = "已上架")] [LabelCssClass(BadgeColorCalss.Success)]
        Online = 2,

        /// <summary>
        ///     审核未通过
        /// </summary>
        [Display(Name = "平台审核失败")] [LabelCssClass(BadgeColorCalss.Danger)]
        FailAudited = 3,

        /// <summary>
        ///     已下架
        /// </summary>
        [Display(Name = "已下架")] [LabelCssClass(BadgeColorCalss.Danger)]
        SoldOut = 4,

        /// <summary>
        ///     供应商删除
        /// </summary>
        [Display(Name = "供应商删除")] [LabelCssClass(BadgeColorCalss.Danger)]
        SupplerDelete = 5,

        /// <summary>
        ///     审核删除
        /// </summary>
        [Display(Name = "平台删除")] [LabelCssClass(BadgeColorCalss.Danger)]
        Deleted = 6
    }
}