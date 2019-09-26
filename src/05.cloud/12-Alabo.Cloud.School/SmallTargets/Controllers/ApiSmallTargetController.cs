using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Cloud.School.SmallTargets.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Controllers {

    [ApiExceptionFilter]
    [Route("Api/SmallTarget/[action]")]
    public class ApiSmallTargetController : ApiBaseController<SmallTarget, ObjectId> {

        public ApiSmallTargetController() : base() {
            BaseService = Resolve<ISmallTargetService>();

        }
    }
}