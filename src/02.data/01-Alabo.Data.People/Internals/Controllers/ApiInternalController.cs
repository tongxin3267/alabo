using Alabo.Data.People.Internals.Domain.Entities;
using Alabo.Data.People.Internals.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.People.Internals.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Internal/[action]")]
    public class ApiInternalController : ApiBaseController<ParentInternal, ObjectId>
    {
        public ApiInternalController()
        {
            BaseService = Resolve<IParentInternalService>();
        }
    }
}