using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "显示位置")]
    public enum ViewLocation
    {
        /// <summary>
        ///     PC端和移动端都显示
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "都显示")]
        All = 1,

        /// <summary>
        ///     仅PC端显示
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "仅PC端显示")]
        OnlyPc = 2,

        /// <summary>
        ///     仅移动端显示
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "仅移动端显示")]
        OnlyMoblie = 3,

        /// <summary>
        ///     全部不显示
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "全部不显示")]
        None = 4
    }
}