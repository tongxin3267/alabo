using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;

namespace Alabo.App.Offline.Merchants.Domain.Dtos
{
    public class MerchantStoreInput
    {
        /// <summary>
        /// 店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(1, 99999999, ErrorMessage = ErrorMessage.NameNotCorrect)]
        public long UserId { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        /// 门店logo
        /// </summary>
        [Display(Name = "门店logo")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Logo { get; set; }

        /// <summary>
        /// 门店描述
        /// </summary>
        [Display(Name = "门店描述")]
        public string Description { get; set; }
    }
}