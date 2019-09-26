using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "商家服务类型")]
    public enum CommentIndustry
    {
        /// <summary>
        ///     通用
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "通用")]
        Common = 0,

        /// <summary>
        ///     旅游
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "旅游")]
        Travel,

        /// <summary>
        ///     餐饮业
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "餐饮")]
        Catering,

        /// <summary>
        ///     服务业
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "服务")]
        Supply
    }
}