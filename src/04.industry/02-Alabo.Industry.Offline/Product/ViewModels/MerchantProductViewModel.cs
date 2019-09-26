using System.Collections.Generic;
using Alabo.Industry.Offline.Merchants.ViewModels;
using Alabo.Industry.Offline.Product.Domain.Entities;

namespace Alabo.Industry.Offline.Product.ViewModels
{
    /// <summary>
    ///     MerchantProductViewModel
    /// </summary>
    public class MerchantProductViewModel
    {
        /// <summary>
        ///     Merchant store info
        /// </summary>
        public MerchantStoreViewModel MerchantStore { get; set; }

        /// <summary>
        ///     Relations
        /// </summary>
        public List<RelationViewModel> Relations { get; set; }

        /// <summary>
        ///     Merchant products
        /// </summary>
        public List<MerchantProduct> Products { get; set; }
    }
}