using System;
using System.Collections.Generic;

namespace Alabo.App.Core.Finance.Domain.Dtos.Pay {

    /// <summary>
    ///     商城支付订单信息
    /// </summary>
    public class PayShopOrderInfo {

        /// <summary>
        ///     订单实际支付的金额
        ///     订单实际支付的金额=商品总金额-优惠金额-（+）调整金额+邮费金额 -其他账户支出
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     使用账户支付的部分
        /// </summary>
        public string AccountPay { get; set; }

        /// <summary>
        ///     支付键值对
        /// </summary>
        public IList<KeyValuePair<Guid, decimal>> AccountPayPair { get; set; } =
            new List<KeyValuePair<Guid, decimal>>();
    }
}