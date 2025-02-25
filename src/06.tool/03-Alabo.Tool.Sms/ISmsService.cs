﻿using Alabo.Dependency;
using System.Threading.Tasks;

namespace Alabo.Tool.Sms
{
    /// <summary>
    ///     短信服务
    /// </summary>
    public interface ISmsService : IScopeDependency
    {
        /// <summary>
        ///     发送短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="content">内容</param>
        Task<SmsResult> SendAsync(string mobile, string content);
    }
}