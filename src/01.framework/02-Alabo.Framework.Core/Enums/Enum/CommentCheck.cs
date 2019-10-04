using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     评论审核
    /// </summary>
    [ClassProperty(Name = "评论审核")]
    public enum CommentCheck {

        /// <summary>
        ///     未审核
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "UnCheck")]
        UnCheck = 0,

        /// <summary>
        ///     已审核
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "Checked")]
        Checked = 1,

        /// <summary>
        ///     已作废
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "Cancelled")]
        Cancelled = 2
    }
}