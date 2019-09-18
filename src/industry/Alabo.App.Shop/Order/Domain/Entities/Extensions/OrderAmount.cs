using Alabo.App.Shop.Order.Domain.Enums;

namespace Alabo.App.Shop.Order.Domain.Entities.Extensions
{

    /// <summary>
    ///     订单价格数据
    /// </summary>
    public class OrderAmount
    {

        /// <summary>
        ///     商品总金额/商品总价值
        ///     商品总金额=所有商品订单价格相加
        /// </summary>
        public decimal TotalProductAmount { get; set; } = 0;

        /// <summary>
        /// 邮费金额(实际邮费)
        /// </summary>
        public decimal ExpressAmount { get; set; } = 0;

        /// <summary>
        /// 邮费金额
        /// </summary>
        public decimal CalculateExpressAmount { get; set; } = 0;

        /// <summary>
        ///     优惠金额（如打折，VIP，满就送等），精确到2位小数
        ///     优惠金额=所有商品所有金额相加
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        ///     卖家手工调整金额，
        ///     调整金额=所有商品调整金额相加
        /// </summary>
        public decimal AdjustAmount { get; set; } = 0;

        /// <summary>
        ///     卖家实际收到的金额，（由于子订单可以部分确认收货，这个金额会随着子订单的确认收货而不断增加，交易成功后等于买家实付款减去退款金额）
        /// </summary>
        public decimal ReceivedAmount { get; set; } = 0;

        /// <summary>
        ///     税费金额
        /// </summary>
        public decimal TaxAmount { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the fee amount.
        ///     服务费
        /// </summary>
        /// <value>The fee amount.</value>
        public decimal FeeAmount { get; set; }

        /// <summary>
        /// 快递方式
        /// </summary>
        public ExpressType ExpressType { get; set; } = ExpressType.Express;

        /// <summary>
        /// 运费说明
        /// </summary>
        public string ExpressDescription { get; set; }
    }
}