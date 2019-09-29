using System.Collections.Generic;

namespace Alabo.App.Asset.Recharges.Dtos
{
    public class RechageAccountOutput
    {
        /// <summary>
        ///     订单使用人民币支付的金额
        ///     是客户实际支付的金额，微信支付宝
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        ///     支付Id
        /// </summary>
        public long PayId { get; set; }

        /// <summary>
        ///     订单Id列表
        /// </summary>
        public List<long> OrderIds { get; set; } = new List<long>();
    }
}