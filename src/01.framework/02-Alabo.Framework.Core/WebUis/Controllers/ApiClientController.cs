using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Core.Common.Controllers {

    /// <summary>
    /// 客户端列表
    /// </summary>
    [Route("Api/Client/[action]")]
    public class ApiClientController : ApiBaseController {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiAController" /> class.
        /// </summary>
        /// <param name="restClientConfig">The rest client configuration.</param>
        public ApiClientController(RestClientConfig restClientConfig
        ) : base() {
        }

        /// <summary>
        /// 获取所有的枚举列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "根据枚举获取KeyValues")]
        public ApiResult<List<EnumList>> EnumList() {
            var list = Resolve<ITypeService>().GetEnumList();

            return ApiResult.Success(list.ToList());
        }
    }
}