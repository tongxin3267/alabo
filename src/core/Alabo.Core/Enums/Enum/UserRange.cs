using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     活动的适用范围
    /// </summary>
    [ClassProperty(Name = "活动适用范围")]
    public enum UserRange
    {
        /// <summary>
        ///     所有会员
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "所有会员")]
        AllUser = 1,

        /// <summary>
        ///     根据会员等级
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "根据会员等级")]
        ByUserGrade = 2
    }
}