using Alabo.Framework.Core.Admins.Repositories;
using Alabo.Framework.Core.Admins.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.Admins.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Admin/[action]")]
    public class ApiAdminController : ApiBaseController
    {
        /// <summary>
        ///     清空缓存
        /// </summary>
        /// <returns></returns>
        public ApiResult ClearCache()
        {
            Resolve<IAdminService>().ClearCache();
            return ApiResult.Success();
        }

        /// <summary>
        ///     初始化权限
        /// </summary>
        [HttpGet]
        public ApiResult Init()
        {
            Ioc.Resolve<ICatalogRepository>().UpdateDataBase();
            // 数据脚本，数据初始等
            // Resolve<IAdminService>().DefaultInit();
            return ApiResult.Success();
        }
    }
}