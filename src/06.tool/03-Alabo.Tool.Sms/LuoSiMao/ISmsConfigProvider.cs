﻿using System.Threading.Tasks;

namespace Alabo.Tool.Sms.LuoSiMao
{
    /// <summary>
    ///     短信配置提供器
    /// </summary>
    public interface ISmsConfigProvider
    {
        /// <summary>
        ///     获取配置
        /// </summary>
        Task<SmsConfig> GetConfigAsync();
    }
}