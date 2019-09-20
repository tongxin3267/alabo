using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    [ClassProperty(Name = "活动开启状态")]
    public enum ActivityStatus
    {
        /// <summary>
        ///     未开始
        /// </summary>
        [Display(Name = "未开始")] [LabelCssClass(BadgeColorCalss.Danger)]
        HasNotStarted = 1,

        /// <summary>
        ///     进行中
        /// </summary>
        [Display(Name = "进行中")] [LabelCssClass(BadgeColorCalss.Success)]
        Processing = 2,

        /// <summary>
        ///     已结束
        /// </summary>
        [Display(Name = "已结束")] [LabelCssClass(BadgeColorCalss.Info)]
        Over = 3
    }
}