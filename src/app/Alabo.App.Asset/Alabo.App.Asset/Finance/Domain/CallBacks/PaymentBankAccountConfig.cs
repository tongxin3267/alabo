using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.CallBacks {

    [NotMapped]
    [ClassProperty(Name = "收款账户", Icon = "fa fa-building", Description = "支付方式", PageType = ViewPageType.List,
        SortOrder = 20)]
    public class PaymentBankAccountConfig {

        /// <summary>
        ///     银行名称
        /// </summary>
        [Display(Name = "银行名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public BankType BankType { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankName { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string BankNumber { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string BankAddress { get; set; }

        /// <summary>
        ///     银行预留信息
        /// </summary>
        [Display(Name = "银行预留信息")]
        public string PhoneNo { get; set; }
    }
}