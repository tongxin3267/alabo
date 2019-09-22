using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.Recharge {

    /// <summary>
    ///     Class ViewRechargeCheck.
    /// </summary>
    public class ViewRechargeCheck : BaseViewModel {

        /// <summary>
        ///     账单失败原因
        /// </summary>
        [Display(Name = "账单失败原因")]
        public string FailuredReason { get; set; }

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        public TradeStatus Status { get; set; }

        /// <summary>
        ///     管理员备注
        /// </summary>
        [Display(Name = "管理员备注")]
        public string Remark { get; set; }
    }
}