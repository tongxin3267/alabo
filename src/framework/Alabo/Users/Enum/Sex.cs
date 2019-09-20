using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     性别
    /// </summary>
    [ClassProperty(Name = "性别")]
    public enum Sex
    {
        /// <summary>
        ///     男
        /// </summary>
        [Display(Name = "男")] [LabelCssClass(BadgeColorCalss.Danger)]
        Man = 1,

        /// <summary>
        ///     女
        /// </summary>
        [Display(Name = "女")] [LabelCssClass(BadgeColorCalss.Info)]
        WoMan = 2,

        /// <summary>
        ///     未知
        /// </summary>
        [Display(Name = "保密")] [LabelCssClass(BadgeColorCalss.Info)]
        UnKnown = 3
    }
}