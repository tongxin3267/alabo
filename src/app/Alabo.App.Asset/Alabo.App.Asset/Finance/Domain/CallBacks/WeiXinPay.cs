namespace Alabo.App.Core.Finance.Domain.CallBacks {

    /// <summary>
    ///     微信支付
    /// </summary>
    public class WeiXinPay {
    }

    public class RefreshResult {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
    }
}