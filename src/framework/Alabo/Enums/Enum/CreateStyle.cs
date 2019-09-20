using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    [ClassProperty(Name = "创建类型")]
    public enum CreateStyle
    {
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "自己创建")]
        CreateSelf = 1,

        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "他人创建")]
        CreateOther = 2
    }
}