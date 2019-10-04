using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ThemePage/[action]")]
    public class ApiThemePageController : ApiBaseController<ThemePage, ObjectId> {

        public ApiThemePageController() {
            BaseService = Resolve<IThemePageService>();
        }
    }
}