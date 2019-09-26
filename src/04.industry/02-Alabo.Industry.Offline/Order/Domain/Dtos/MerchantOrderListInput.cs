using Alabo.Industry.Offline.Order.Domain.Enums;

namespace Alabo.Industry.Offline.Order.Domain.Dtos
{
    public class MerchantOrderListInput
    {
        /// <summary>
        ///     订单状态
        /// </summary>
        public MerchantOrderStatus OrderStatus { get; set; }

        /// <summary>
        ///     获取会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     店铺Id
        /// </summary>
        public string MerchantStoreId { get; set; }

        /// <summary>
        ///     当前页 private
        /// </summary>
        public long PageIndex { get; set; }

        /// <summary>
        ///     每页记录数 private
        /// </summary>

        public long PageSize { get; set; }
    }
}