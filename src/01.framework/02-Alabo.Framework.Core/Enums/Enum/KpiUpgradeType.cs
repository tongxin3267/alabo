using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     会员自动升级类型
    /// </summary>
    [ClassProperty(Name = "会员自动升级类型")]
    public enum KpiUpgradeType
    {
        /// <summary>
        ///     推荐会员
        /// </summary>
        [Display(Name = "直推会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        RecommendUser = 1,

        /// <summary>
        ///     中国银行
        /// </summary>
        [Display(Name = "间接推荐会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        SecondUser = 2,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "团队会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        TeamUser = 3,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "直推会员+间接推荐会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        RecommendAndSecondUser = 11,

        /// <summary>
        ///     直推会员+团队会员
        /// </summary>
        [Display(Name = "直推会员+团队会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        RecommendUserAndTeamUser = 12,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "间接推荐会员+团队会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        SecondUserAndTeamUser = 13,

        /// <summary>
        ///     团队会员
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)] [Display(Name = "直推会员 或者 间接推荐会员")]
        RecommendAOrSecondUser = 21,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "直推会员 或者 团队会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        RecommendUserOrTeamUser = 22,

        /// <summary>
        ///     团队会员
        /// </summary>
        [Display(Name = "间接推荐会员 或者 团队会员")] [LabelCssClass(BadgeColorCalss.Danger)]
        SecondUserOrTeamUser = 23,

        /// <summary>
        ///     升级点
        /// </summary>
        [Display(Name = "升级点")] [LabelCssClass(BadgeColorCalss.Danger)]
        UpgradePoint = 90
    }
}