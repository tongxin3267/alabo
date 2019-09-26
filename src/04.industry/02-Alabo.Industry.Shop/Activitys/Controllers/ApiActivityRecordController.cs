using Microsoft.AspNetCore.Mvc;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Services;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ActivityRecord/[action]")]
    public class ApiActivityRecordController : ApiBaseController<ActivityRecord, long> {

        public ApiActivityRecordController() : base() {
            BaseService = Resolve<IActivityRecordService>();
        }
    }
}