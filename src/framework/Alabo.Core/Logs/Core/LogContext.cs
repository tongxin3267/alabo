using System;
using System.Diagnostics;
using Alabo.Core.Logs.Abstractions;
using Alabo.Core.Logs.Internal;

namespace Alabo.Core.Logs.Core
{
    /// <summary>
    ///     日志上下文
    /// </summary>
    public class LogContext : ILogContext
    {
        /// <summary>
        ///     日志上下文信息
        /// </summary>
        private readonly LogContextInfo _info;

        /// <summary>
        ///     跟踪号
        /// </summary>
        public string TraceId => GetInfo().TraceId;

        /// <summary>
        ///     计时器
        /// </summary>
        public Stopwatch Stopwatch => GetInfo().Stopwatch;

        /// <summary>
        ///     IP
        /// </summary>
        public string Ip => GetInfo().Ip;

        /// <summary>
        ///     主机
        /// </summary>
        public string Host => GetInfo().Host;

        /// <summary>
        ///     浏览器
        /// </summary>
        public string Browser => GetInfo().Browser;

        /// <summary>
        ///     请求地址
        /// </summary>
        public string Url => GetInfo().Url;

        /// <summary>
        ///     获取日志上下文信息
        /// </summary>
        private LogContextInfo GetInfo()
        {
            return null;
            //if (_info != null)
            //    return _info;
            //var key = "Util.Logs.LogContext";
            //_info = Context.Get<LogContextInfo>(key);
            //if (_info != null)
            //    return _info;
            //_info = CreateInfo();
            //Context.Add(key, _info);
            //return _info;
        }

        /// <summary>
        ///     创建日志上下文信息
        /// </summary>
        protected virtual LogContextInfo CreateInfo()
        {
            return new LogContextInfo
            {
                TraceId = GetTraceId(),
                Stopwatch = GetStopwatch()
                // Ip = Web.Ip,
                // Host = Web.Host,
                // Browser = Web.Browser,
                // Url = Web.Url
            };
        }

        /// <summary>
        ///     获取跟踪号
        /// </summary>
        protected string GetTraceId()
        {
            var traceId = string.Empty;
            return string.IsNullOrWhiteSpace(traceId) ? Guid.NewGuid().ToString() : traceId;
        }

        /// <summary>
        ///     获取计时器
        /// </summary>
        protected Stopwatch GetStopwatch()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }
    }
}