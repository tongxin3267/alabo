using Alabo.Domains.Entities;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.RestfulApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.WebUis.Controllers
{
    /// <summary>
    ///     客户端列表
    /// </summary>
    [Route("Api/Client/[action]")]
    public class ApiClientController : ApiBaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiAController" /> class.
        /// </summary>
        /// <param name="restClientConfig">The rest client configuration.</param>
        public ApiClientController(RestClientConfig restClientConfig
        )
        {
        }

        /// <summary>
        ///     获取所有的枚举列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "根据枚举获取KeyValues")]
        public ApiResult<List<EnumList>> EnumList()
        {
            var list = Resolve<ITypeService>().GetEnumList();

            return ApiResult.Success(list.ToList());
        }
    }
}