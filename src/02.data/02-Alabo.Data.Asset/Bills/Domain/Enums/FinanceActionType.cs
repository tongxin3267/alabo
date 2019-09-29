using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Bills.Domain.Enums
{
    /// <summary>
    ///     FinanceActionType
    /// </summary>
    [ClassProperty(Name = "资金变化类型")]
    public enum FinanceActionType
    {
        /// <summary>
        ///     增加
        /// </summary>
        [Display(Name = "增加")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Add,

        /// <summary>
        ///     减少
        /// </summary>
        [Display(Name = "减少")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Reduce,

        /// <summary>
        ///     冻结
        /// </summary>
        [Display(Name = "冻结")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Freeze,

        /// <summary>
        ///     解冻
        /// </summary>
        [Display(Name = "解冻")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Unfreeze
    }
}