using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     评论类型
    /// </summary>
    [ClassProperty(Name = "评论类型")]
    public enum CommentType
    {
        /// <summary>
        ///     文章评论
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "文章评论")]
        Article = 0,

        /// <summary>
        ///     商品评论
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "商品评论")]
        Product = 1,

        /// <summary>
        ///     旅游产品评论
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "旅游产品评论")]
        Trip = 2,

        /// <summary>
        ///     一元夺宝评论
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "一元夺宝评论")]
        Yiyuan = 3
    }
}