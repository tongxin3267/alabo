using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alabo.App.Offline.Order.Domain.Enums
{
    /// <summary>
    /// MerchantOrderType
    /// </summary>
    public enum MerchantOrderType
    {
        /// <summary>
        /// 普通订单
        /// </summary>
        [Display(Name = "普通订单")]
        Normal = 1,
    }
}
