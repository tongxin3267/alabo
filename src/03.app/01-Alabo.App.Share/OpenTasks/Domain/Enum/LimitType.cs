using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.OpenTasks.Domain.Enum
{
    [ClassProperty(Name = "按时间限制方式")]
    public enum LimitType
    {
        /// <summary>
        ///     不封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "不封顶")]
        None = 1,

        /// <summary>
        ///     封顶但无时间限制
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "封顶但无时间限制")]
        TotalValue = 2,

        /// <summary>
        ///     按天封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "按天封顶")]
        Days = 3,

        /// <summary>
        ///     按周封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "按周封顶")]
        Weeks = 4,

        /// <summary>
        ///     按月封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "按月封顶")]
        Months = 5,

        /// <summary>
        ///     按季度封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "按季度封顶")]
        Quarters = 6,

        /// <summary>
        ///     按年封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "按年封顶")]
        Years = 7
    }
}