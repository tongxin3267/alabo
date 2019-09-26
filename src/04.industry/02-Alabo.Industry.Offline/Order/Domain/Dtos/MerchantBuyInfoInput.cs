using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Industry.Offline.Order.ViewModels;
using Alabo.Validations;

namespace Alabo.Industry.Offline.Order.Domain.Dtos
{
    /// <summary>
    ///     MerchantBuyInfoInput
    /// </summary>
    public class MerchantBuyInfoInput
    {
        /// <summary>
        ///     User id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long UserId { get; set; }

        /// <summary>
        ///     Merchant store id
        /// </summary>
        [Display(Name = "门店id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string MerchantStoreId { get; set; }

        /// <summary>
        ///     Product items
        /// </summary>
        public List<MerchantCartViewModel> ProductItems { get; set; }
    }
}