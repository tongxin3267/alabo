using Alabo.Data.Targets.Iterations.Domain.Entities;
using Alabo.Data.Targets.Iterations.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Iterations.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Iteration/[action]")]
    public class ApiIterationController : ApiBaseController<Iteration, ObjectId>
    {
        public ApiIterationController() : base() {
            BaseService = Resolve<IIterationService>();
        }
    }
}