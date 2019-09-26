namespace Alabo.App.Asset.Transfers.Dtos {

    public class TransferDetail {

        /// <summary>
        ///     id
        /// </summary>
        public long Id { get; set; }

        public string Serial { get; set; }

        public decimal Account { get; set; }

        /// <summary>
        ///     Api 接口返回 包含转入账户（金额）
        /// </summary>
        public string InMoenyTypeIntr { get; set; }

        /// <summary>
        ///     Api 接口返回 包含转出账户（金额）
        /// </summary>
        public string OutMoenyTypeIntr { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        ///     转账类型名称（费率）
        /// </summary>
        public string TransferConfigIntr { get; set; }

        /// <summary>
        ///     交易时间
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        ///     转账状态
        /// </summary>
        public string Status { get; set; }

        public string TragetUserName { get; set; }

        public string Message { get; set; }
    }
}