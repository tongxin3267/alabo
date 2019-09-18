using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Finance.ViewModels.WithDraw {

    /// <summary>
    ///     Class ViewWithdraw.
    /// </summary>
    public class ViewWithdraw : BaseViewModel {

        /// <summary>
        ///     银行卡ID
        /// </summary>
        [Display(Name = "银行卡")]
        public long BankCardid { get; set; }

        /// <summary>
        ///     提现额度
        /// </summary>
        [RegularExpression(@"^([1-9]\d*\.\d*|0\.\d*[1-9]\d*)|([1-9]\d*)$", ErrorMessage = "提现额度不能小于0.1")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "提现额度")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        public string UseRemark { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }
    }
}