using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Special/[action]")]
    public class ApiSpecialController : ApiBaseController<Special, ObjectId> {

        public ApiSpecialController() : base() {
            BaseService = Resolve<ISpecialService>();
        }
    }
}