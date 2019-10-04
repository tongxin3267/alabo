using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     团队类型
    /// </summary>
    [ClassProperty(Name = "团队类型")]
    public enum KpiTeamType {

        /// <summary>
        ///     会员本身
        /// </summary>
        [Display(Name = "会员本身")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        UserSelf = 1,

        /// <summary>
        ///     推荐会员
        /// </summary>
        [Display(Name = "直推会员")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        RecommendUser = 11,

        /// <summary>
        ///     中国银行
        /// </summary>
        [Display(Name = "间接推荐会员")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        SecondUser = 12,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "团队会员")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        TeamUser = 31
    }
}