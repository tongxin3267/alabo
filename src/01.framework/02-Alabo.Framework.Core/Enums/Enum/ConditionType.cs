using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "条件类型")]
    public enum ConditionType
    {
        /// <summary>
        ///     条件是金额，比如订单多少金额，商品多少金额
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "条件是金额，比如订单多少金额，商品多少金额")]
        Amount = 1,

        /// <summary>
        ///     条件是数量，比如订单满足多少件
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "条件是数量，比如订单满足多少件")]
        Number = 2
    }
}