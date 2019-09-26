using System;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.AutoConfigs.Services;
using Alabo.Framework.Core.WebApis.Configs;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebApis.Controller {

    public abstract class ApiBaseController : ApiExtensionController {
        protected AccessToken Token;

        public ApiBaseController() {
        }

        /// <summary>
        /// executing
        /// </summary>
        /// <param name="context"></param>
        [NonAction]
        public override void OnActionExecuting(ActionExecutingContext context) {
            // api接口可以匿名访问，不需要任何安全措施
            if (context.Filters.OfType<IAllowAnonymousFilter>().Any()) {
                return;
            }
            //基础api接口权限检查
            var token = GetAccessToken();
            if (token.Status != ResultStatus.Success) {
                context.Result = new JsonResult(ApiResult.Failure(token.Message, token.MessageCode));
            }
            // 用户权限检查，比如管理员、会员、供应商等权限的检查
            token = UserAccessToken(context);
            if (token.Status != ResultStatus.Success) {
                context.Result = new JsonResult(ApiResult.Failure(token.Message, token.MessageCode));
            }
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 用户权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [NonAction]
        protected ApiResult<AccessToken> UserAccessToken(ActionExecutingContext context) {
            var apiAuthAttribute = context.Filters.OfType<ApiAuthAttribute>().FirstOrDefault();
            if (apiAuthAttribute == null && AutoModel.Filter == FilterType.All) {
                return ApiResult.Success(new AccessToken());
            }
            if (apiAuthAttribute == null) {
                apiAuthAttribute = new ApiAuthAttribute {
                    Filter = FilterType.All
                };
            }
            // 其他情况需要用户登录
            if (apiAuthAttribute.Filter != FilterType.All || AutoModel.Filter != FilterType.All) {
                //if (User == null)
                //{
                //    return ApiResult.Failure<AccessToken>("请在http头中传入userId");
                //}

                //var tokenSign = HttpContext.Request.Headers["zk-token"].ToString();
                //tokenSign = tokenSign.Substring(2, 10) + Resolve<IAlaboUserService>().GetUserToken(User);
                //var md5Token = tokenSign.ToLower().ToMd5HashString();

                //var userToken = HttpContext.Request.Headers["zk-user-token"];
                //if (md5Token != userToken)
                //{
                //    return ApiResult.Failure<AccessToken>($"用户token计算错误,加密因子不匹配");
                //}

                // 管理员权限判断
                // 开放该判断后 供应商无法访问订单和商品列表
                //if (apiAuthAttribute.Filter == FilterType.Admin || AutoModel.Filter == FilterType.Admin)
                //{
                //    if (!Resolve<IAlaboUserService>().IsAdmin(User.Id))
                //    {
                //        return ApiResult.Failure<AccessToken>($"您不是管理员无权访问");
                //    }
                //}
            }
            return ApiResult.Success(new AccessToken());
        }

        #region Api接口权限基础验证

        /// <summary>
        /// Api接口权限基础验证
        /// </summary>
        /// <returns></returns>
        [NonAction]
        protected ApiResult<AccessToken> GetAccessToken() {
            var apiSecurityConfig = Resolve<IAlaboAutoConfigService>().GetValue<ApiSecurityConfig>();
            //如果是开发模式，这不验证安全
            if (!apiSecurityConfig.IsOpen) {
                return ApiResult.Success(new AccessToken());
            }

            var maxTimestamp = DateTime.Now.AddMinutes(10).ConvertDateTimeInt();
            var minTimestamp = DateTime.Now.AddMinutes(-10).ConvertDateTimeInt();
            var token = HttpContext.Request.Headers["zk-token"];
            var timestamp = HttpContext.Request.Headers["zk-timestamp"];
            var apiUrl = HttpContext.Request.Path.ToStr();
            apiUrl = apiUrl.ToLower().Replace("/api/", "api/");
            if (token.IsNullOrEmpty()) {
                return ApiResult.Failure<AccessToken>("请在http头中传入token");
            }

            if (timestamp.IsNullOrEmpty()) {
                return ApiResult.Failure<AccessToken>("请在http头中传入timestamp");
            }

            if (timestamp.ConvertToLong() < minTimestamp || timestamp.ConvertToLong() > maxTimestamp) {
                return ApiResult.Failure<AccessToken>($"时间错计算错误，服务器当前时间{DateTime.Now}");
            }

            var tokenSign = apiUrl + timestamp + RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Id +
                            HttpWeb.Tenant + apiSecurityConfig?.PrivateKey.Trim();
            tokenSign = tokenSign.ToLower();
            var md5Token = tokenSign.ToMd5HashString();

            if (md5Token != token) {
                return ApiResult.Failure<AccessToken>($"token计算错误,服务器计算,加密因子不匹配");
            }

            return ApiResult.Success(new AccessToken());
        }

        #endregion Api接口权限基础验证
    }
}