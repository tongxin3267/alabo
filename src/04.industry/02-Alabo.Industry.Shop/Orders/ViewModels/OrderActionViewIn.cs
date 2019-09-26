namespace Alabo.Industry.Shop.Orders.ViewModels
{
    /// <summary>
    ///     订单操作View
    /// </summary>
    public class OrderActionViewIn
    {
        /// <summary>
        ///     订单ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     支付密码
        /// </summary>
        public string PayPassword { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public long UserId { get; set; }
    }
}