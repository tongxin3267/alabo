using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/About/[action]")]
    public class ApiAboutController : ApiBaseController<About, ObjectId> {

        public ApiAboutController() : base() {
            BaseService = Resolve<IAboutService>();
        }
    }
}