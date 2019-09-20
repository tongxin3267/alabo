using Microsoft.AspNetCore.Mvc.Filters;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Api.Filter {

    public class ApiSecurityAttribute : ExceptionFilterAttribute {

        /// <summary>
        ///     安全级别
        /// </summary>
        public SecurityLevel SecurityLevel { get; set; } = SecurityLevel.Client;

        public override void OnException(ExceptionContext context) {
            base.OnException(context);
            //ApiResult exceptionApiResult = null;
            //if (RuntimeContext.Current.WebsiteConfig.IsDevelopment) {
            //    exceptionApiResult = ApiResult.Error(context.Exception.ToString(), context.Exception.Message);
            //} else {
            //    exceptionApiResult = ApiResult.Error(context.Exception.Message);
            //}

            //context.Result = new JsonResult(exceptionApiResult) {
            //    StatusCode = (int)HttpStatusCode.BadRequest
            //};
            context.ExceptionHandled = true;
        }
    }

    /// <summary>
    ///     安全级别
    /// </summary>
    [ClassProperty(Name = "安全级别")]
    public enum SecurityLevel {

        /// <summary>
        ///     ZkWeb客户端可以访问
        /// </summary>
        Client = 1,

        /// <summary>
        ///     ZKOpen项目，可以访问
        /// </summary>
        Open = 2,

        /// <summary>
        ///     后台可以访问
        /// </summary>
        Admin = 3
    }
}