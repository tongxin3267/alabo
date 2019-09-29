using Alabo.Framework.Basic.Letters.Domain.Entities;
using Alabo.Framework.Basic.Letters.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Letters.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Letter/[action]")]
    public class ApiLetterController : ApiBaseController<Letter, ObjectId>
    {
        public ApiLetterController()
        {
            BaseService = Resolve<ILetterService>();
        }
    }
}