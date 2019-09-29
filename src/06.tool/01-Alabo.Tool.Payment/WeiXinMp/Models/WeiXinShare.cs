namespace Alabo.Tool.Payment.WeiXinMp.Models
{
    /// <summary>
    ///     微信分享
    /// </summary>
    public class WeiXinShare
    {
        public string AppId { get; set; }

        /// <summary>
        ///     时间戳
        /// </summary>
        public string Timestamp { get; set; }

        public string AccessToken { get; set; }

        public string Ticket { get; set; }

        public string NonceStr { get; set; }

        public string Signature { get; set; }

        /// <summary>
        ///     图片链接地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        ///     网址
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    ///     分享内容
    /// </summary>
    public class WeiXinShareInput
    {
        /// <summary>
        ///     网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     标题
        /// </summary>

        public string Title { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     图片
        /// </summary>
        public string ImageUrl { get; set; }
    }
}