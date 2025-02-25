﻿using Alabo.Exceptions;
using Alabo.Extensions;
using System;

namespace Alabo.Logging.Extensions {

    /// <summary>
    ///     异常扩展
    /// </summary>
    public static partial class Extensions {

        /// <summary>
        ///     写日志
        /// </summary>
        /// <param name="exception">异常</param>
        /// <param name="log">日志</param>
        public static void Log(this Exception exception, ILog log) {
            exception = exception.GetRawException();
            if (exception is ValidException warning) {
                log.Exception(exception, warning.Code).Warn();
                return;
            }

            log.Exception(exception).Error();
        }
    }
}