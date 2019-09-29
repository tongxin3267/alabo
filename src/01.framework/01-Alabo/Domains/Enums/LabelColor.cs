using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     显示样式
    ///     http://ui.5ug.com/metronic_v4.5.4/theme/admin_4/ui_general.html
    /// </summary>
    [ClassProperty(Name = "显示样式")]
    public enum LabelColor
    {
        [Display(Name = "灰色")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Default = 1,

        [Display(Name = "深蓝")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Primary = 2,

        [Display(Name = "浅蓝")]
        [LabelCssClass(BadgeColorCalss.Info)]
        Info = 3,

        [Display(Name = "绿色")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Success = 4,

        [Display(Name = "红色")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Danger = 5,

        [Display(Name = "黄色")]
        [LabelCssClass("m-badge-warning")]
        Warning = 6,

        [Display(Name = "黄色")]
        [LabelCssClass("m-badge-warning")]
        Brand = 7
    }
}