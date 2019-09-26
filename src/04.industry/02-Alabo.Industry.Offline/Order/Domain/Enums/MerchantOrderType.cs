using System.ComponentModel.DataAnnotations;

namespace Alabo.Industry.Offline.Order.Domain.Enums
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
