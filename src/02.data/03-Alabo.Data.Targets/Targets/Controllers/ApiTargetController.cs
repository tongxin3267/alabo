using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Data.Targets.Targets.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Targets.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Target/[action]")]
    public class ApiTargetController : ApiBaseController<Target, ObjectId>
    {
        public ApiTargetController() : base() {
            BaseService = Resolve<ITargetService>();
        }
    }
}