using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     评论级别
    /// </summary>
    [ClassProperty(Name = "评论级别")]
    public enum CommentLevel {

        /// <summary>
        ///     好评
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "好评")]
        Good = 0,

        /// <summary>
        ///     中评
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "中评")]
        Middle = 1,

        /// <summary>
        ///     差评
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "差评")]
        Bad = 2
    }
}