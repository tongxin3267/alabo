using System;
using System.Collections.Generic;
using System.Text;

namespace Alabo.App.Offline.Order.Domain.Dtos
{
    /// <summary>
    /// MerchantOrderBuyOutput
    /// </summary>
    public class MerchantOrderBuyOutput
    {
        /// <summary>
        /// Pay id
        /// </summary>
        public long PayId { get; set; }

        /// <summary>
        /// Pay amount
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// order ids
        /// </summary>
        public List<long> OrderIds { get; set; } = new List<long>();
    }
}
