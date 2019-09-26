using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.TaskExecutes.Domain.Enums
{
    /// <summary>
    ///     封顶方式
    /// </summary>
    [ClassProperty(Name = "封顶方式")]
    public enum LimitType
    {
        /// <summary>
        ///     不封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "不封顶")]
        NoMaxValue = 1,

        /// <summary>
        ///     封顶但无时间限制
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "封顶但无时间限制")]
        MaxValueNoTimeLimit = 2,

        /// <summary>
        ///     按天封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "按天封顶")]
        MaxValueByDay = 3,

        /// <summary>
        ///     按周封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "按周封顶")]
        MaxValueByWeek = 4,

        /// <summary>
        ///     按月封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "按月封顶")]
        MaxValueByMonth = 5,

        /// <summary>
        ///     按季度封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "按季度封顶")]
        MaxValueByQuarter = 6,

        /// <summary>
        ///     按年封顶
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "按年封顶")]
        MaxValueByYear = 7
    }
}