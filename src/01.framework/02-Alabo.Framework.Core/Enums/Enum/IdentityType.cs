using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "认证类型")]
    public enum IdentityType
    {
        /// <summary>
        ///     个人
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "个人")]
        Personal = 1,

        /// <summary>
        ///     企业
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "企业")]
        Company = 2
    }
}