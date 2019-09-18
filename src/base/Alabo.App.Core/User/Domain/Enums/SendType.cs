using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.Domain.Enums {

    [ClassProperty(Name = "从属关系类型")]
    public enum SendType {

        /// <summary>
        ///     指定用户
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "指定用户")]
        SpecificUser = 0,

        /// <summary>
        ///     指定类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "指定类型")]
        NamedType = 1,

        /// <summary>
        ///     所有
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "所有")]
        All = 2
    }
}