using System.ComponentModel.DataAnnotations;

namespace Alabo.Industry.Offline.Merchants.ViewModels
{
    public class MerchantStoreViewModel
    {
        /// <summary>
        ///     店铺id
        /// </summary>
        [Display(Name = "店铺id")]
        public string Id { get; set; }

        /// <summary>
        ///     店铺名称
        /// </summary>
        [Display(Name = "店铺名称")]
        public string Name { get; set; }

        /// <summary>
        ///     门店logo
        /// </summary>
        [Display(Name = "门店logo")]
        public string Logo { get; set; }

        /// <summary>
        ///     门店描述
        /// </summary>
        [Display(Name = "门店描述")]
        public string Description { get; set; }
    }
}