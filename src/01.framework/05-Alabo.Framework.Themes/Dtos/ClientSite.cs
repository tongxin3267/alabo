namespace Alabo.Framework.Themes.Dtos {

    /// <summary>
    /// 客户端站点设置
    /// </summary>
    public class ClientSite {

        /// <summary>
        /// 是否开通微信支付
        /// </summary>
        public bool IsWeiXinPay {
            get; set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 租户信息
        /// </summary>
        public string Tenant { get; set; }

        /// <summary>
        /// 没有进行微信登录认证，需要进行微信授权认证,在进行微信授权时候有一点需要注意，提供给微信的授权回调URL只能带一个参数
        /// WX_APP_ID 微信公众号的appid 保存在config 配置文件
        /// </summary>
        public string WeiXinUrl { get; set; } =
            "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state=STATE#wechat_redirect";

        /// <summary>
        /// 支付宝授权相关网址
        ///    authorizeUrl = 'https://openauth.alipay.com/oauth2/publicAppAuthorize.htm?app_id=' + process.env.ALI_APP_ID + '&scope=auth_user&redirect_uri=' + encodeURI(currentUrl)
        /// </summary>
        public string AlipayUrl { get; set; }
    }
}