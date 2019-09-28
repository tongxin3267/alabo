using System;
using System.Collections.Generic;
using Alabo.Domains.Repositories.Mongo.Extension;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     店铺订单价格，与总订单价格
    /// </summary>
    public class StoreOrderPrice
    {
        /// <summary>
        ///     订单总价格
        /// </summary>
        public decimal TotalAmount { get; set; } = 0.0m;

        /// <summary>
        ///     订单总运费
        /// </summary>
        public decimal ExpressAmount { get; set; } = 0.0m;

        /// <summary>
        ///     订单商品总价
        /// </summary>
        public decimal ProductAmount { get; set; } = 0.0m;

        /// <summary>
        ///     Gets or sets the fee amount.
        ///     订单服务费
        /// </summary>
        /// <value>The fee amount.</value>
        public decimal FeeAmount { get; set; }

        /// <summary>
        ///     返回店铺价格 ID,总价
        /// </summary>

        public IList<StorePrice> StorePrices { get; set; } = new List<StorePrice>();

        /// <summary>
        ///     Gets or sets the store moneys. 店铺扣除金额
        /// </summary>
        /// <value> The store moneys. </value>
        public IList<OrderMoneyItem> ReduceMoneys { get; set; } = new List<OrderMoneyItem>();
    }

    /// <summary>
    ///     店铺资产数据
    /// </summary>
    public class OrderMoneyItem
    {
        /// <summary>
        ///     Gets or sets the money identifier.
        /// </summary>
        /// <value> The money identifier. </value>
        public Guid MoneyId { get; set; }

        /// <summary>
        ///     Gets or sets the intro.
        /// </summary>
        /// <value> The intro. </value>
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value> The description. </value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the name. 货币类型名称
        /// </summary>
        /// <value> The name. </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the maximum pay amount. 最高可支付的金额
        ///     虚拟资产，最高可使用虚拟资产支付的价格
        /// </summary>
        /// <value> The maximum pay amount. </value>
        public decimal MaxPayPrice { get; set; }

        /// <summary>
        ///     Gets or sets the minimum pay cash. 现金的最低支付额度
        /// </summary>
        /// <value> The minimum pay cash. </value>
        public decimal MinPayCash { get; set; }

        /// <summary>
        ///     Gets or sets the balance. 账户余额
        /// </summary>
        /// <value> The balance. </value>
        public decimal Balance { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        ///     减少或扣除的金额
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }
    }

    /// <summary>
    ///     店铺价格
    /// </summary>
    public class StorePrice
    {
        /// <summary>
        ///     店铺Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        /// <summary>
        ///     实际店铺运费
        /// </summary>
        public decimal ExpressAmount { get; set; } = 0.0m;

        /// <summary>
        ///     计算店铺运费
        /// </summary>
        public decimal CalculateExpressAmount { get; set; } = 0.0m;

        /// <summary>
        ///     商品总价
        /// </summary>
        public decimal ProductAmount { get; set; } = 0.0m;

        /// <summary>
        ///     店铺总价
        /// </summary>
        public decimal TotalAmount { get; set; } = 0.0m;

        /// <summary>
        ///     Gets or sets the total weight.
        /// </summary>
        /// <value> The total weight. </value>
        public decimal TotalWeight { get; set; } = 0.0m;

        /// <summary>
        ///     Gets or sets the fee amount.
        ///     订单服务费
        /// </summary>
        /// <value>The fee amount.</value>
        public decimal FeeAmount { get; set; }

        /// <summary>
        ///     member discount amount
        /// </summary>
        public decimal MemberDiscountAmount { get; set; } = 0.0m;
    }
}