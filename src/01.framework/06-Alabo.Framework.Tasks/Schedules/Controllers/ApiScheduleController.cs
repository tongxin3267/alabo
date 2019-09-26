using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Services;

namespace Alabo.App.Core.Tasks.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Schedule/[action]")]
    public class ApiScheduleController : ApiBaseController<Schedule, ObjectId> {

        public ApiScheduleController() : base() {
            BaseService = Resolve<IScheduleService>();
        }
    }
}