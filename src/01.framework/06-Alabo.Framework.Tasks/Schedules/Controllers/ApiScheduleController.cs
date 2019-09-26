using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using Alabo.Framework.Tasks.Schedules.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Alabo.Framework.Tasks.Schedules.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Schedule/[action]")]
    public class ApiScheduleController : ApiBaseController<Schedule, ObjectId> {

        public ApiScheduleController() : base() {
            BaseService = Resolve<IScheduleService>();
        }
    }
}