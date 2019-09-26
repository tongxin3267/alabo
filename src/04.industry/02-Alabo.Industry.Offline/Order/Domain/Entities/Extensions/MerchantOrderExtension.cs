using System.Collections.Generic;
using Alabo.Domains.Entities.Extensions;
using Alabo.Industry.Offline.Order.ViewModels;

namespace Alabo.Industry.Offline.Order.Domain.Entities.Extensions
{
    /// <summary>
    ///     订单扩展数据
    /// </summary>
    public class MerchantOrderExtension : EntityExtension
    {
        /// <summary>
        ///     Order amount
        /// </summary>
        public MerchantOrderAmount OrderAmount { get; set; }

        /// <summary>
        ///     Merchant products
        /// </summary>
        public List<MerchantCartViewModel> MerchantProducts { get; set; }
    }

    /// <summary>
    ///     Merchant order amount
    /// </summary>
    public class MerchantOrderAmount
    {
        /// <summary>
        ///     实际金额
        /// </summary>
        public decimal ReceivedAmount { get; set; }

        /// <summary>
        ///     商品总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     优惠金额（如打折，VIP，满就送等），精确到2位小数
        ///     优惠金额=所有商品所有金额相加
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        ///     邮费金额
        /// </summary>
        public decimal ExpressAmount { get; set; }

        /// <summary>
        ///     Gets or sets the fee amount.
        /// </summary>
        /// <value>The fee amount.</value>
        public decimal FeeAmount { get; set; }
    }
}