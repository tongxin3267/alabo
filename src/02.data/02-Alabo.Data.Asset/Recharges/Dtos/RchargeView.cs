using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Recharge {

    /// <summary>
    ///     Class RchargeView.
    /// </summary>
    public class RchargeView : BaseViewModel {

        /// <summary>
        ///     Gets or sets the action 类型.
        /// </summary>
        [Display(Name = "账单操作类型")]
        public BillActionType ActionType { get; set; } = BillActionType.Recharge;

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "货币种类")]
        public Currency Currency { get; set; }

        /// <summary>
        ///     <summary>
        ///         会员备注
        ///     </summary>
        [Display(Name = "备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     充值方式:线上、线下
        /// </summary>
        public long RchargeStyle { get; set; }
    }
}