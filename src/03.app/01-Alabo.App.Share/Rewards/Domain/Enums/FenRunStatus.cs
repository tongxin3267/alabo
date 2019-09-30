using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.Rewards.Domain.Enums
{
    /// <summary>
    ///     分润状态
    /// </summary>
    [ClassProperty(Name = "分润状态")]
    public enum FenRunStatus
    {
        /// <summary>
        ///     待确认
        /// </summary>
        [Display(Name = "待确认")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        TobeConfirm = 1,

        /// <summary>
        ///     已成功
        /// </summary>
        [Display(Name = "已成功")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Success = 3,

        /// <summary>
        ///     已取消
        /// </summary>
        [Display(Name = "已取消")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Cancel = 4
    }
}