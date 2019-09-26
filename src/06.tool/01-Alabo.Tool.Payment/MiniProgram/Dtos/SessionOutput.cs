namespace Alabo.Tool.Payment.MiniProgram.Dtos
{
    /// <summary>
    ///     微信小程序登录后返回的信息
    /// </summary>
    public class SessionOutput
    {
        /// <summary>
        ///     用户唯一标识
        /// </summary>
        /// <value>The open identifier.</value>
        public string openid { get; set; }

        /// <summary>
        ///     Gets or sets the session key. 会话密钥
        /// </summary>
        /// <value>The session key.</value>
        public string session_key { get; set; }

        ///// <summary>
        ///// 用户在开放平台的唯一标识符。本字段在满足一定条件的情况下才返回。 具体参看UnionID机制说明https://mp.weixin.qq.com/debug/wxadoc/dev/api/uinionID.html
        ///// </summary>
        ///// <value>The unionid.</value>
        //public string UnionId { get; set; }
    }
}