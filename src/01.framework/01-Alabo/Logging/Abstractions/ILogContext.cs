﻿using System.Diagnostics;

namespace Alabo.Logging.Abstractions {

    /// <summary>
    ///     日志上下文
    /// </summary>
    public interface ILogContext {

        /// <summary>
        ///     跟踪号
        /// </summary>
        string TraceId { get; }

        /// <summary>
        ///     计时器
        /// </summary>
        Stopwatch Stopwatch { get; }

        /// <summary>
        ///     IP
        /// </summary>
        string Ip { get; }

        /// <summary>
        ///     主机
        /// </summary>
        string Host { get; }

        /// <summary>
        ///     浏览器
        /// </summary>
        string Browser { get; }

        /// <summary>
        ///     请求地址
        /// </summary>
        string Url { get; }
    }
}