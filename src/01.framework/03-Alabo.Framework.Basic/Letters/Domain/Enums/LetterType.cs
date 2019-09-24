using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ZKCloud.App.Open.Attach.Domain.Enums {

    /// <summary>
    /// 站内信
    /// </summary>
    [ClassProperty(Name = "站内信")]
    public enum LetterType {

        /// <summary>
        ///     系统消息
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "系统消息")]
        System = 1,

        /// <summary>
        ///     普通消息
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "普通消息")]
        Ordinary = 2
    }
}