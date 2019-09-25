using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    /// <summary>
    ///     Class BuyInfoInput.
    /// </summary>
    public class BuyInfoInput : EntityDto {

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long LoginUserId { get; set; }

        /// <summary>
        ///     产品参数不能为空
        /// </summary>
        [Display(Name = "产品参数")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string ProductJson { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is buy.
        ///     是否是购买的时候使用
        ///     如果是则生成缓存，加快价格的计算
        /// </summary>
        public bool IsBuy { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        /// </summary>
        public bool IsGroupBuy { get; set; }
    }
}