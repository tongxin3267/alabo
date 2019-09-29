using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Basic.Letters.Domain.Enums
{
    [ClassProperty(Name = "发送目标")]
    public enum SendType
    {
        /// <summary>
        ///     指定用户
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "指定用户")]
        SpecificUser = 1,

        /// <summary>
        ///     指定类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "指定类型")]
        NamedType = 2,

        /// <summary>
        ///     所有
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)] [Display(Name = "所有")]
        All = 3
    }
}