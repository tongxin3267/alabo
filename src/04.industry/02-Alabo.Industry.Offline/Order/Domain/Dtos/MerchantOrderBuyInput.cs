using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Industry.Offline.Order.ViewModels;
using Alabo.Tool.Payment;
using Alabo.Validations;

namespace Alabo.Industry.Offline.Order.Domain.Dtos
{
    /// <summary>
    ///     MerchantOrderBuyInput
    /// </summary>
    public class MerchantOrderBuyInput
    {
        /// <summary>
        ///     Gets or sets user id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     Merchant store id
        /// </summary>
        [Display(Name = "店铺id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string MerchantStoreId { get; set; }

        /// <summary>
        ///     Total amount
        /// </summary>
        [Display(Name = "订单总金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0.00, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     Total count
        /// </summary>
        [Display(Name = "订单商品总数")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public long TotalCount { get; set; }

        /// <summary>
        ///     Pay amount
        /// </summary>
        [Display(Name = "支付金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0.01, long.MaxValue, ErrorMessage = ErrorMessage.NameNotInRang)]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     Pay type
        /// </summary>
        [Display(Name = "支付方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public PayType PayType { get; set; }

        /// <summary>
        ///     Products
        /// </summary>
        public List<MerchantCartViewModel> Products { get; set; }
    }
}