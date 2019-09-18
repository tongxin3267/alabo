using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Enums {

    /// <summary>
    /// 审核状态
    /// </summary>
    public enum ReviewState {

        /// <summary>
        /// 通过
        /// </summary>
        [Display(Name = "通过")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Pass = 1,

        /// <summary>
        /// 拒绝
        /// </summary>
        [Display(Name = "拒绝")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Reject
    }
}