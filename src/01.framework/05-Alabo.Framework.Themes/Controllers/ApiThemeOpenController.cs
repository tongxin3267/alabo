using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Themes.Domain.Services;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Extensions;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Themes.Controllers {

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