using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Share.Domain.Enums {

    /// <summary>
    /// 分润触发类型
    /// </summary>
    [ClassProperty(Name = "分润触发类型")]
    public enum TeamlLivenessType {

        /// <summary>
        /// 自身
        /// </summary>
        [Display(Name = "总团队")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Self = 0,

        /// <summary>
        /// 子团队
        /// </summary>
        [Display(Name = "子团队")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Child
    }
}