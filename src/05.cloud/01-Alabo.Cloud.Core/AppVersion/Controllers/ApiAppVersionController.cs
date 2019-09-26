using _01_Alabo.Cloud.Core.AppVersion.Domain.Configs;
using _01_Alabo.Cloud.Core.AppVersion.Domain.Enums;
using _01_Alabo.Cloud.Core.AppVersion.Dtos;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace _01_Alabo.Cloud.Core.AppVersion.Controllers
{
    /// <summary>
    ///     ApiStore Api接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/SendSms/[action]")]
    public class ApiAppVersionController : ApiBaseController
    {
        /// <summary>
        ///     APP版本检测
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<AppVersionOutput> AppCheckVersion(AppVersionInput input)
        {
            var config = Resolve<IAutoConfigService>().GetValue<AppVersionConfig>();
            //获取服务器app当前版本
            if (config.IsEnble && !input.Version.Equals(config.Version)) //最新版本
                return new ApiResult<AppVersionOutput>
                {
                    Result = new AppVersionOutput
                    {
                        Note = config.Note,
                        Status = AppVersionStatus.Use,
                        Url = config.Url
                    },
                    Status = ResultStatus.Success,
                    Message = string.Empty,
                    MessageCode = 200
                };
            return new ApiResult<AppVersionOutput>
            {
                Result = new AppVersionOutput
                {
                    Status = AppVersionStatus.UnUp
                },
                Status = ResultStatus.Success,
                Message = string.Empty,
                MessageCode = 200
            };
        }
    }
}