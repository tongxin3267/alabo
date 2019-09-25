using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.App.Core.Finance.Domain.Dtos.Transfer {

    public class TransferApiInput : ApiInputDto {

        /// <summary>
        ///     Gets or sets the name of the other 会员.
        /// </summary>
        [Display(Name = "对方用户")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string OtherUserName { get; set; }

        /// <summary>
        ///     Gets or sets the transfer identifier.
        /// </summary>
        [Display(Name = "转出")]
        public Guid TransferId { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Display(Name = "货币种类")]
        public Currency Currency { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        [RegularExpression(@"^([1-9]\d*\.\d*|0\.\d*[1-9]\d*)|([1-9]\d*)$", ErrorMessage = "转账金额不能小于0.1")]
        [Required(ErrorMessage = "请输入转账金额")]
        [Display(Name = "转账金额")]
        public decimal Amount { get; set; }

        /// <summary>
        ///     会员备注
        /// </summary>
        [Display(Name = "备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     Gets or sets the pay password.
        /// </summary>
        [Display(Name = "支付密码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string PayPassword { get; set; }
    }
}