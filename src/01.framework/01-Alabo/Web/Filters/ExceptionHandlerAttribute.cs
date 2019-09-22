using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Alabo.Extensions;
using Alabo.Runtime;
using Alabo.Web.Mvc.Exception;

namespace Alabo.Web.Filters {

    /// <summary>
    ///     异常处理过滤器
    /// </summary>
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute {

        /// <summary>
        ///     异常处理
        /// </summary>
        public override void OnException(ExceptionContext context) {
            base.OnException(context);

            if (context.ExceptionHandled == false) {
                var msg = context.Exception.Message;
                msg = msg.ReplaceHtmlTag().ToReplace().ToUrlEncode();
                msg = msg.ReplaceHtmlTag().ToEncoding();

                var path = context.HttpContext.Request.Path.ToString().Replace("/", "_");

                var fullPath = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
                ExceptionLogs.Write(context.Exception, path, fullPath);

                ;
            }

            context.ExceptionHandled = true;

            //context.ExceptionHandled = true;
            //context.HttpContext.Response.StatusCode = 200;
            //context.Result = new Result(StateCode.Fail, context.Exception.Message);
            //// context.Result = new Result(StateCode.Fail, context.Exception.GetPrompt());
        }
    }
}