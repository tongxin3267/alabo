using Alabo.App.Offline.Order.ViewModels;
using System.Collections.Generic;

namespace Alabo.App.Offline.Order.Domain.Dtos {

    /// <summary>
    /// MerchantCartOutput
    /// </summary>
    public class MerchantCartOutput {

        /// <summary>
        /// Total count
        /// </summary>
        public long TotalCount { get; set; } = 0;

        /// <summary>
        /// Total amount
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Fee amount
        /// </summary>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public List<MerchantCartViewModel> Products { get; set; } = new List<MerchantCartViewModel>();
    }
}