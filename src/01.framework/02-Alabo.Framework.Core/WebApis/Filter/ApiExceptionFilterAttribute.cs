using Alabo.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Filter {

    /// <summary>
    ///     Api异常过滤接口
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute {

        public override void OnException(ExceptionContext context) {
            base.OnException(context);
            ApiResult exceptionApiResult = null;
            if (RuntimeContext.Current.WebsiteConfig.IsDevelopment) {
                exceptionApiResult = ApiResult.Error(context.Exception.ToString(), context.Exception.Message);
            } else {
                var message = context.Exception.Message;
                if (context.Exception.InnerException != null) {
                    message = context.Exception.InnerException.Message;
                }

                exceptionApiResult = ApiResult.Error(message);
            }

            context.Result = new JsonResult(exceptionApiResult) {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            context.ExceptionHandled = true;
        }
    }
}