using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.UI.Enum
{
    /// <summary>
    ///     显示样式
    ///     http://ui.5ug.com/metronic_v4.5.4/theme/admin_4/ui_general.html
    /// </summary>
    [ClassProperty(Name = "显示样式")]
    public enum LabelStyle
    {
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "自己创建")]
        Label = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "自己创建")]
        Badges = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "自己创建")]
        RoundlessBadges = 3
    }
}