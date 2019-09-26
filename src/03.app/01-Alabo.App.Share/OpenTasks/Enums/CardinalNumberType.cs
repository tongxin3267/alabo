using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.OpenTasks.Enums
{
    /// <summary>
    ///     分润基数类型
    /// </summary>
    [ClassProperty(Name = "分润基数类型")]
    public enum CardinalNumberType
    {
        /// <summary>
        ///     指定数额
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "指定数额")]
        FixedAmount = 1,

        /// <summary>
        ///     个人业绩
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "个人业绩")]
        PersonalAchievement = 2,

        /// <summary>
        ///     团队业绩
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "团队业绩")]
        TeamAchievement = 3
    }
}