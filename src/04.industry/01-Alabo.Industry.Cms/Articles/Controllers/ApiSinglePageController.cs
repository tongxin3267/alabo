using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/SinglePage/[action]")]
    public class ApiSinglePageController : ApiBaseController<SinglePage, ObjectId> {

        public ApiSinglePageController() : base() {
            BaseService = Resolve<ISinglePageService>();
        }
    }
}