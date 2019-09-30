using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Tables.Domain.Services;
using Alabo.Tool.Payment.MiniProgram.Dtos;
using Alabo.Tool.Payment.MiniProgram.Services;
using Alabo.Tool.Payment.WeiXinMp.Models;
using Alabo.Tool.Payment.WeiXinMp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Tool.Payment.Controllers
{
    /// <summary>
    ///     ApiStore Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/ApiStore/[action]")]
    public class ApiStoreController : ApiBaseController
    {
        /// <summary>
        ///     微信小程序登录，微信公众号登录
        ///     根据后台设置确定是否获取头像
        /// </summary>
        /// <param name="jsCode">The js code.</param>
        [HttpGet]
        [Display(Description = "微信小程序登录，微信公众号登录")]
        public ApiResult<LoginOutput> Login([FromQuery] string jsCode)
        {
            var miniProgramLoginInput = new LoginInput
            {
                JsCode = jsCode
            };
            return Resolve<IMiniProgramService>().Login(miniProgramLoginInput);
        }

        [HttpGet]
        [Display(Description = "微信公众号登录")]
        public ApiResult<LoginOutput> WeixinPubLogin([FromQuery] string jsCode)
        {
            var miniProgramLoginInput = new LoginInput
            {
                JsCode = jsCode
            };

            return Resolve<IMiniProgramService>().PubLogin(miniProgramLoginInput);
        }

        /// <summary>
        ///     微信分享
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "微信分享")]
        public ApiResult<WeiXinShare> Share(WeiXinShareInput shareInput)
        {
            if (shareInput.Url.IsNullOrEmpty()) return ApiResult.Failure<WeiXinShare>("分享网址不能为空");

            //Resolve<IUserService>().Log("分享" + shareInput.ToJsons());

            var webSite = Resolve<IAlaboAutoConfigService>().GetValue<WebSiteConfig>();

            try
            {
                var result = Resolve<IWeixinMpService>().WeiXinShare(shareInput);
                return ApiResult.Success(result);
            }
            catch (Exception exception)
            {
                Resolve<ITableService>().Log("微信分享接口获取错误" + exception.Message);
                return ApiResult.Failure<WeiXinShare>(exception.Message);
            }
        }
    }
}