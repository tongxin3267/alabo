using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Pays.Dtos
{
    /// <summary>
    ///     客户端类型
    /// </summary>
    public class ClientInput
    {
        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long LoginUserId { get; set; }

        /// <summary>
        ///     终端类型
        /// </summary>
        [Display(Name = "终端类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public ClientType BrowserType { get; set; }

        /// <summary>
        ///     支付金额
        /// </summary>
        [Display(Name = "支付金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0.01, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal Amount { get; set; }

        /// <summary>
        ///     支付订单Id
        /// </summary>
        [Display(Name = "支付订单Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long PayId { get; set; }
    }
}