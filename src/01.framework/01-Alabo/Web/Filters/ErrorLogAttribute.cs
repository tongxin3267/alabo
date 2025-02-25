﻿using Alabo.Logging;
using Alabo.Logging.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Alabo.Web.Filters {

    /// <summary>
    ///     错误日志过滤器
    /// </summary>
    public class ErrorLogAttribute : ExceptionFilterAttribute {

        /// <summary>
        ///     异常处理
        /// </summary>
        public override void OnException(ExceptionContext context) {
            WriteLog(context);
        }

        /// <summary>
        ///     异常处理
        /// </summary>
        public override Task OnExceptionAsync(ExceptionContext context) {
            WriteLog(context);
            return Task.CompletedTask;
        }

        /// <summary>
        ///     记录错误日志
        /// </summary>
        private void WriteLog(ExceptionContext context) {
            if (context == null) {
                return;
            }

            var log = Log.GetLog(context).Caption("WebApi全局异常");
            context.Exception.Log(log);
        }
    }
}