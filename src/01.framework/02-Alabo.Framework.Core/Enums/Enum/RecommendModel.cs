using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "推荐方式")]
    public enum RecommendModel
    {
        /// <summary>
        ///     任意推荐
        ///     会员推荐任意其他类型的会员
        /// </summary>
        [Display(Name = "任意推荐")]
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true)]
        AnyRecommend = 0,

        /// <summary>
        ///     优先级高推荐
        ///     也叫接点会员
        /// </summary>
        [Display(Name = "优先级高推荐")]
        [LabelCssClass(BadgeColorCalss.Success)]
        PriorityHighRecommend = 1,

        /// <summary>
        ///     优先级低
        /// </summary>
        [Display(Name = "优先级低")]
        [LabelCssClass(BadgeColorCalss.Success)]
        PriorityLowRecommend = 2
    }
}