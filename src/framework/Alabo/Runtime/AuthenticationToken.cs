using System;

namespace Alabo.Runtime
{
    /// <summary>
    ///     Class AuthenticationToken.
    /// </summary>
    public class AuthenticationToken
    {
        /// <summary>
        ///     是否认证通过
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        ///     认证token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     激活时间
        /// </summary>
        public DateTime AuthenticationTime { get; set; }

        /// <summary>
        ///     激活服务端信息（如果激活失败，会有原因信息）
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     激活认证结果消息码
        /// </summary>
        public int MessageCode { get; set; }

        /// <summary>
        ///     本次激活过期时间
        /// </summary>
        public DateTime ExpiresTime { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}