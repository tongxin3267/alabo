using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Withdraws.Domain.Enums
{
    /// <summary>
    ///     审核状态
    /// </summary>
    public enum ReviewState
    {
        /// <summary>
        ///     通过
        /// </summary>
        [Display(Name = "通过")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Pass = 1,

        /// <summary>
        ///     拒绝
        /// </summary>
        [Display(Name = "拒绝")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Reject
    }
}