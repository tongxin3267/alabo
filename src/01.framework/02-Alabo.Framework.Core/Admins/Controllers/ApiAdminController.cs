using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Admin {

    [ApiExceptionFilter]
    [Route("Api/Admin/[action]")]
    public class ApiAdminController : ApiBaseController {

        public ApiAdminController() : base() {
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        /// <returns></returns>
        public ApiResult ClearCache() {
            Resolve<IAdminService>().ClearCache();
            return ApiResult.Success();
        }

        /// <summary>
        ///     初始化权限
        /// </summary>
        [HttpGet]
        public ApiResult Init() {
            // 数据脚本，数据初始等
            Resolve<IAdminService>().DefaultInit(false);
            return ApiResult.Success();
        }
    }
}