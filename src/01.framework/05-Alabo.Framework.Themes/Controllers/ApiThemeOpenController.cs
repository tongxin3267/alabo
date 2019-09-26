using System.ComponentModel.DataAnnotations;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Themes.Domain.Services;
using Alabo.Framework.Themes.Dtos.Service;
using Alabo.Helpers;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Themes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ThemeOpen/[action]")]
    public class ApiThemeOpenController : ApiBaseController {

        public ApiThemeOpenController() : base() {
        }

        /// <summary>
        ///     发布
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "站点发布")]
        public ApiResult PublishAsync([FromBody] ThemePublish parameter) {
            var tenant = HttpWeb.Tenant;
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            var result = Resolve<IThemeOpenService>().Publish(parameter);
            return ToResult(result);
        }
    }
}