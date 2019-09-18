using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Domain.Services;

namespace Alabo.App.Core.Themes.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ThemePage/[action]")]
    public class ApiThemePageController : ApiBaseController<ThemePage, ObjectId> {

        public ApiThemePageController() : base() {
            BaseService = Resolve<IThemePageService>();
        }
    }
}