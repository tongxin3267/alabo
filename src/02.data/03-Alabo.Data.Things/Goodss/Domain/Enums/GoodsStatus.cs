using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Things.Goodss.Domain.Enums
{
    /// <summary>
    ///     平台商品状态
    /// </summary>
    public enum GoodsStatus
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
        ///     已下架
        /// </summary>
        [Display(Name = "已下架")] [LabelCssClass(BadgeColorCalss.Danger)]
        SoldOut = 3,

        /// <summary>
        ///     审核删除
        /// </summary>
        [Display(Name = "已删除")] [LabelCssClass(BadgeColorCalss.Danger)]
        Deleted = 4
    }
}