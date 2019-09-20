using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    /// <summary>
    ///     资金流向
    /// </summary>
    [ClassProperty(Name = "资金流向")]
    public enum AccountFlow
    {
        /// <summary>
        ///     支出
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)] [Display(Name = "支出")]
        Spending = 1,

        /// <summary>
        ///     收入
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "收入")]
        Income = 2
    }
}