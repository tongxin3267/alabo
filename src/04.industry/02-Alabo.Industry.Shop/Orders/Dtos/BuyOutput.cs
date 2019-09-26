using Newtonsoft.Json;
using System.Collections.Generic;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Shop.Order.Domain.Dtos {

    /// <summary>
    ///     订单购买数据传输
    /// </summary>
    public class BuyOutput : EntityDto {

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