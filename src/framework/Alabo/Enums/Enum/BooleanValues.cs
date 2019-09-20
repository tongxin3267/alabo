using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     布尔值
    /// </summary>
    [ClassProperty(Name = "布尔值")]
    public enum BooleanValues
    {
        /// <summary>
        ///     是
        /// </summary>
        [Display(Name = "是")] [LabelCssClass(BadgeColorCalss.Success)]
        True = 1,

        /// <summary>
        ///     否
        /// </summary>
        [Display(Name = "否")] [LabelCssClass(BadgeColorCalss.Metal)]
        False = 0
    }
}