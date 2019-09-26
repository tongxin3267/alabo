using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.App.Core.ApiStore.MiniProgram.Services;
using Alabo.App.Core.ApiStore.WeiXinMp.Models;
using Alabo.App.Core.ApiStore.WeiXinMp.Services;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Services;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.ApiStore.Controllers {

    /// <summary>
    ///     ApiStore Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/ApiStore/[action]")]
    public class ApiStoreController : ApiBaseController {

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="ApiStoreController" /> class.
        /// </summary>
        public ApiStoreController(
            ) : base() {
        }

        /// <summary>
        /// 微信小程序登录，微信公众号登录
        /// 根据后台设置确定是否获取头像
        /// </summary>
        /// <param name="jsCode">The js code.</param>
        [HttpGet]
        [Display(Description = "微信小程序登录，微信公众号登录")]
        public ApiResult<LoginOutput> Login([FromQuery] string jsCode) {
            var miniProgramLoginInput = new LoginInput {
                JsCode = jsCode
            };
            return Resolve<IMiniProgramService>().Login(miniProgramLoginInput);
        }

        [HttpGet]
        [Display(Description = "微信公众号登录")]
        public ApiResult<LoginOutput> WeixinPubLogin([FromQuery] string jsCode) {
            var miniProgramLoginInput = new LoginInput {
                JsCode = jsCode
            };

            return Resolve<IMiniProgramService>().PubLogin(miniProgramLoginInput);
        }

        /// <summary>
        /// 微信分享
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "微信分享")]
        public ApiResult<WeiXinShare> Share(WeiXinShareInput shareInput) {
            if (shareInput.Url.IsNullOrEmpty()) {
                return ApiResult.Failure<WeiXinShare>("分享网址不能为空");
            }

            //Resolve<IUserService>().Log("分享" + shareInput.ToJsons());

            var webSite = Resolve<IAlaboAutoConfigService>().GetValue<WebSiteConfig>();

            try {
                var result = Resolve<IWeixinMpService>().WeiXinShare(shareInput);
                return ApiResult.Success(result);
            } catch (Exception exception) {
                Resolve<ITableService>().Log("微信分享接口获取错误" + exception.Message);
                return ApiResult.Failure<WeiXinShare>(exception.Message);
            }
        }
    }
}