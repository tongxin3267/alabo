namespace Alabo.App.Shop.Order.ViewModels {

    public class OrderAccountPay {

        /// <summary>
        ///     用户的账户信息
        /// </summary>
        public long AccountId { get; set; }

        /// <summary>
        ///     其他账户支付的数量
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     转换比率
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        ///     相对人民币总支出金额，
        /// </summary>
        public decimal TotalPay => Amount * Rate;
    }
}