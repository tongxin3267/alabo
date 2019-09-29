using Alabo.App.Asset.Withdraws.Domain.Enums;
using Alabo.Web.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Withdraws.Dtos
{
    /// <summary>
    ///     Class ViewWithDrawCheck.
    /// </summary>
    public class ViewWithDrawCheck : BaseViewModel
    {
        /// <summary>
        ///     账单失败原因
        /// </summary>
        [Display(Name = "账单失败原因")]
        public string FailuredReason { get; set; }

        /// <summary>
        ///     Gets or sets the trade identifier.
        /// </summary>
        public long TradeId { get; set; }

        /// <summary>
        ///     管理员备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        public WithdrawStatus Status { get; set; }

        /// <summary>
        ///     申请提现额度
        /// </summary>
        [Display(Name = "申请提现额度")]
        public decimal Amount { get; set; }
    }
}