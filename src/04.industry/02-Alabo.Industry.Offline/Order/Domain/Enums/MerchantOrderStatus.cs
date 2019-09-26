using System.ComponentModel.DataAnnotations;

namespace Alabo.Industry.Offline.Order.Domain.Enums
{
    /// <summary>
    /// MerchantOrderStatus
    /// </summary>
    public enum MerchantOrderStatus
    {
        /// <summary>
        /// Waiting to pay
        /// </summary>
        [Display(Name = "待付款")]
        WaitingBuyerPay = 1,

        /// <summary>
        /// Success
        /// </summary>
        [Display(Name = "已完成")]
        Success = 2,

        /// <summary>
        /// Closed
        /// </summary>
        [Display(Name = "已关闭")]
        Closed = 3
    }
}
