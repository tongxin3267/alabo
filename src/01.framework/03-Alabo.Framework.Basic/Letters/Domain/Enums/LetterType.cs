using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Basic.Letters.Domain.Enums
{
    /// <summary>
    ///     站内信
    /// </summary>
    [ClassProperty(Name = "站内信")]
    public enum LetterType
    {
        /// <summary>
        ///     系统消息
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "系统消息")]
        System = 1,

        /// <summary>
        ///     普通消息
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "普通消息")]
        Ordinary = 2
    }
}