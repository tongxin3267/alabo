﻿using Alabo.Contexts;
using Alabo.Helpers;
using Alabo.Logging.Abstractions;
using Alabo.Logging.Internal;
using System.Diagnostics;

namespace Alabo.Logging.Core {

    /// <summary>
    ///     日志上下文
    /// </summary>
    public class LogContext : ILogContext {

        /// <summary>
        ///     日志上下文信息
        /// </summary>
        private LogContextInfo _info;

        /// <summary>
        ///     序号
        /// </summary>
        private int _orderId;

        /// <summary>
        ///     初始化日志上下文
        /// </summary>
        /// <param name="context">上下文</param>
        public LogContext(IContext context) {
            Context = context;
            _orderId = 0;
        }

        /// <summary>
        ///     上下文
        /// </summary>
        public IContext Context { get; set; }

        /// <summary>
        ///     跟踪号
        /// </summary>
        public string TraceId => $"{GetInfo().TraceId}-{++_orderId}";

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
        private LogContextInfo GetInfo() {
            if (_info != null) {
                return _info;
            }

            var key = "Util.Logs.LogContext";
            _info = Context.Get<LogContextInfo>(key);
            if (_info != null) {
                return _info;
            }

            _info = CreateInfo();
            Context.Add(key, _info);
            return _info;
        }

        /// <summary>
        ///     创建日志上下文信息
        /// </summary>
        protected virtual LogContextInfo CreateInfo() {
            return new LogContextInfo {
                TraceId = GetTraceId(),
                Stopwatch = GetStopwatch(),
                Ip = HttpWeb.Ip,
                Host = HttpWeb.Host,
                Browser = HttpWeb.Browser,
                Url = HttpWeb.Url
            };
        }

        /// <summary>
        ///     获取跟踪号
        /// </summary>
        protected string GetTraceId() {
            var traceId = Context.TraceId;
            return string.IsNullOrWhiteSpace(traceId) ? Id.Guid() : traceId;
        }

        /// <summary>
        ///     获取计时器
        /// </summary>
        protected Stopwatch GetStopwatch() {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            return stopwatch;
        }
    }
}