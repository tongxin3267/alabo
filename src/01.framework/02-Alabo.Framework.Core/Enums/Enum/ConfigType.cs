using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "配置类型")]
    public enum ConfigType
    {
        [Display(Name = "多配置")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Configs = 0,

        [Display(Name = "单配置")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Config = 1
    }
}