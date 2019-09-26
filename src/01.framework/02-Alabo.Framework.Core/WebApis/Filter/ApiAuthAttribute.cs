using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Runtime;
using Alabo.UI;

namespace Alabo.Framework.Core.WebApis.Filter {

    /// <summary>
    ///     Api 接口，用户登录授权
    /// </summary>
    public class ApiAuthAttribute : ExceptionFilterAttribute {

        /// <summary>
        /// Api接口数据过滤方式
        /// 默认Api接口权限为用户级别
        /// </summary>
        public FilterType Filter { get; set; } = FilterType.User;

        public override void OnException(ExceptionContext context) {
            base.OnException(context);
            ApiResult exceptionApiResult = null;
            if (RuntimeContext.Current.WebsiteConfig.IsDevelopment) {
                exceptionApiResult = ApiResult.Error(context.Exception.ToString(), context.Exception.Message);
            } else {
                exceptionApiResult = ApiResult.Error(context.Exception.Message);
            }

            context.Result = new JsonResult(exceptionApiResult) {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
            context.ExceptionHandled = true;
        }
    }
}