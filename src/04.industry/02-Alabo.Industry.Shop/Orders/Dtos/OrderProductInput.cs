using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    /// <summary>
    ///     订单商品信息
    /// </summary>
    public class OrderProductInput : EntityDto {

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long LoginUserId { get; set; }

        /// <summary>
        ///     商品Sku
        /// </summary>
        [Display(Name = "商品Sku")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long ProductSkuId { get; set; }

        /// <summary>
        ///     商品Id
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the store identifier.
        ///     店铺Id
        /// </summary>
        /// <value>
        ///     The store identifier.
        /// </value>
        public long StoreId { get; set; }

        /// <summary>
        ///     商品数量
        /// </summary>
        [Display(Name = "数量")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long Count { get; set; }
    }
}