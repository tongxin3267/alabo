using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Enums {

    /// <summary>
    ///     财务交易类型
    /// </summary>
    [ClassProperty(Name = "财务交易类型")]
    public enum TradeType {

        /// <summary>
        ///     线上充值
        /// </summary>
        [Display(Name = "充值")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Recharge = 1,

        /// <summary>
        ///     提现
        /// </summary>
        [Display(Name = "提现")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Withraw = 2,

        /// <summary>
        ///     转账
        /// </summary>
        [Display(Name = "转账")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Transfer = 3,

        /// <summary>
        ///     现金支付
        /// </summary>
        [Display(Name = "现金支付")]
        [LabelCssClass(BadgeColorCalss.Primary)]
        Payment = 4,
    }
}