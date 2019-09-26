namespace Alabo.App.Asset.Pays.Dtos {

    public class WxMessage {
        public string DeviceInfo { get; set; }
        public string SignType { get; set; }
        public string OpenId { get; set; }
        public string IsSubscribe { get; set; }
        public string TradeType { get; set; }
        public string BankType { get; set; }
        public string TotalFee { get; set; }
        public string SettlementTotalFee { get; set; }
        public string FeeType { get; set; }
        public string CashFee { get; set; }
        public string CashFeeType { get; set; }
        public string CouponFee { get; set; }
        public string CouponCount { get; set; }
        public string TransactionId { get; set; }
        public string OutTradeNo { get; set; }
        public string Attach { get; set; }
        public string TimeEnd { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMsg { get; set; }
        public string AppId { get; set; }
        public string MchId { get; set; }
        public string NonceStr { get; set; }
        public string Sign { get; set; }
        public string ResultCode { get; set; }
        public string ErrCode { get; set; }
        public string ErrCodeDes { get; set; }
        public bool IsError { get; set; }
        public string ReqInfo { get; set; }
        public string Body { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class Parameters {
        public string appid { get; set; }
        public string attach { get; set; }
        public string bank_type { get; set; }
        public string cash_fee { get; set; }
        public string fee_type { get; set; }
        public string is_subscribe { get; set; }
        public string mch_id { get; set; }
        public string nonce_str { get; set; }
        public string openid { get; set; }
        public string out_trade_no { get; set; }
        public string result_code { get; set; }
        public string return_code { get; set; }
        public string sign { get; set; }
        public string time_end { get; set; }
        public string total_fee { get; set; }
        public string trade_type { get; set; }
        public string transaction_id { get; set; }
    }
}