using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.App.Core.Finance.Domain.Dtos.Pay {

    /// <summary>
    ///     支付订单信息
    /// </summary>
    public class PayInput : EntityDto {

        /// <summary>
        ///     Id
        /// </summary>
        [Display(Name = "支付记录Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long PayId {
            get; set;
        }

        /// <summary>
        ///     支付方式
        /// </summary>
        [Display(Name = "支付方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public PayType PayType {
            get; set;
        }

        /// <summary>
        ///     Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long LoginUserId {
            get; set;
        }

        /// <summary>
        ///     支付金额,积分方式的时候，可能为0
        /// </summary>
        [Display(Name = "支付金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public decimal Amount {
            get; set;
        }

        /// <summary>
        /// 前台跳转链接，为空的时候
        /// 跳转到订单详情页
        /// </summary>
        public string RedirectUrl { get; set; } = string.Empty;

        /// <summary>
        ///     微信用户OpenId
        /// </summary>
        [Display(Name = "微信用户OpenId")]
        public string OpenId {
            get; set;
        }
    }
}