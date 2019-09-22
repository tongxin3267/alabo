using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Themes.Domain.Enums {

    [ClassProperty(Name = "窗体打开方式")]
    public enum Target {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "当前窗口打开")]
        _self = 0,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "新窗口打开")]
        _blank = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "父窗口打开")]
        _parent = 2
    }
}