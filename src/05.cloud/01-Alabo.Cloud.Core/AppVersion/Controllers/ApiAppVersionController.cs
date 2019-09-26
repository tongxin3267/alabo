using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.ApiStore.AppVersion.Entities;
using Alabo.App.Core.ApiStore.AppVersion.Models;
using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.App.Core.ApiStore.MiniProgram.Services;
using Alabo.App.Core.ApiStore.Sms.Entities;
using Alabo.App.Core.ApiStore.Sms.Enums;
using Alabo.App.Core.ApiStore.Sms.Models;
using Alabo.App.Core.ApiStore.WeiXinMp.Models;
using Alabo.App.Core.ApiStore.WeiXinMp.Services;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.App.Core.ApiStore.Controllers {

    /// <summary>
    ///     ApiStore Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/SendSms/[action]")]
    public class ApiAppVersionController : ApiBaseController {

        /// <summary>
        /// APP版本检测
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<AppVersionOutput> AppCheckVersion(AppVersionInput input) {
            var config = Resolve<IAutoConfigService>().GetValue<AppVersionConfig>();
            //获取服务器app当前版本
            if (config.IsEnble && !input.Version.Equals(config.Version)) {
                //最新版本
                return new ApiResult<AppVersionOutput>() {
                    Result = new AppVersionOutput() {
                        Note = config.Note,
                        Status = AppVersion.Enums.AppVersionStatus.Use,
                        Url = config.Url
                    },
                    Status = ResultStatus.Success,
                    Message = string.Empty,
                    MessageCode = 200
                };
            } else {
                return new ApiResult<AppVersionOutput>() {
                    Result = new AppVersionOutput() {
                        Status = AppVersion.Enums.AppVersionStatus.UnUp,
                    },
                    Status = ResultStatus.Success,
                    Message = string.Empty,
                    MessageCode = 200
                };
            }
        }
    }
}