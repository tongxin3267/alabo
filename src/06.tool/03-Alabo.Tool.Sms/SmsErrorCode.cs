﻿using System.ComponentModel;

namespace Alabo.Tool.Sms
{
    /// <summary>
    ///     短信错误码
    /// </summary>
    public enum SmsErrorCode
    {
        /// <summary>
        ///     发送成功
        /// </summary>
        Ok,

        /// <summary>
        ///     手机号错误
        /// </summary>
        [Description("手机号错误")] MobileError
    }
}