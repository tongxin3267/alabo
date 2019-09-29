using System.Collections.Generic;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     货款输入
    /// </summary>
    public class PayGoodsAmountInput
    {
        /// <summary>
        ///     订单id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        ///     凭证 最多三个
        /// </summary>
        public List<string> Icertificate { get; set; }
    }
}