using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/About/[action]")]
    public class ApiAboutController : ApiBaseController<About, ObjectId>
    {
        public ApiAboutController()
        {
            BaseService = Resolve<IAboutService>();
        }
    }
}