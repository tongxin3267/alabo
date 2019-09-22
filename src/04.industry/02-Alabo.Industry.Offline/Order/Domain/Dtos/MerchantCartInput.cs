using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Domains.Entities;

namespace Alabo.App.Offline.Order.Domain.Dtos
{
    /// <summary>
    /// MerchantCartInput
    /// </summary>
    public class MerchantCartInput
    {
        /// <summary>
        /// cart Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Merchant store id
        /// </summary>
        public string MerchantStoreId { get; set; }

        /// <summary>
        /// User id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long UserId { get; set; }

        /// <summary>
        /// Merchant product id
        /// </summary>
        public string MerchantProductId { get; set; }

        /// <summary>
        /// SkuId
        /// </summary>
        [Display(Name = "SkuId")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string SkuId { get; set; }

        /// <summary>
        /// Count
        /// </summary>
        [Display(Name = "数量")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long Count { get; set; }
    }
}
