using System.Collections.Generic;
using Alabo.App.Offline.Merchants.ViewModels;
using Alabo.App.Offline.Product.Domain.Entities;

namespace Alabo.App.Offline.Product.ViewModels
{
    /// <summary>
    /// MerchantProductViewModel
    /// </summary>
    public class MerchantProductViewModel
    {
        /// <summary>
        /// Merchant store info
        /// </summary>
        public MerchantStoreViewModel MerchantStore { get; set; }

        /// <summary>
        /// Relations
        /// </summary>
        public List<RelationViewModel> Relations { get; set; }

        /// <summary>
        /// Merchant products
        /// </summary>
        public List<MerchantProduct> Products { get; set; }
    }
}