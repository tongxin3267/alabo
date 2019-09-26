using System;
using Alabo.Industry.Shop.Products.Dtos;

namespace Alabo.Industry.Shop.Orders.Domain.Entities.Extensions {

    /// <summary>
    ///     订单商品表，扩展属性
    /// </summary>
    public class OrderProductExtension {

        /// <summary>
        ///     Gets or sets the store moneys. 店铺扣除金额
        /// </summary>
        /// <value> The store moneys. </value>
        public ReduceAmount ReduceAmount { get; set; } = new ReduceAmount();

        /// <summary>
        ///     订单价格信息
        /// </summary>
        public OrderAmount OrderAmount { get; set; } = new OrderAmount();

        /// <summary>
        ///     Gets or sets the total weight.
        /// </summary>
        /// <value>
        ///     The total weight.
        /// </value>
        public decimal TotalWeight { get; set; }

        /// <summary>
        ///     Gets or sets the product sku.
        /// </summary>
        /// <value>
        ///     The product sku.
        /// </value>
        public ProductSkuItem ProductSkuItem { get; set; } = new ProductSkuItem();
    }

    /// <summary>
    ///     订单商品金额
    /// </summary>
    public class ReduceAmount {

        /// <summary>
        ///     Gets or sets the money type identifier.
        ///     货币类型Id
        /// </summary>
        /// <value>
        ///     The money type identifier.
        /// </value>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        ///     货币类型减少金额
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets for cash amount.
        ///     抵现金额
        /// </summary>
        /// <value>
        ///     For cash amount.
        /// </value>
        public decimal ForCashAmount { get; set; }

        /// <summary>
        ///     Gets or sets the name of the money.
        /// </summary>
        /// <value>
        ///     The name of the money.
        /// </value>
        public string MoneyName { get; set; }

        /// <summary>
        ///     服务费金额(虚拟资产）
        /// </summary>
        public decimal FeeAmount { get; set; } = 0.0m;
    }
}