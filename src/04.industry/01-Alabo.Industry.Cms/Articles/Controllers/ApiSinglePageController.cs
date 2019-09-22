using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/SinglePage/[action]")]
    public class ApiSinglePageController : ApiBaseController<SinglePage, ObjectId> {

        public ApiSinglePageController() : base() {
            BaseService = Resolve<ISinglePageService>();
        }
    }
}