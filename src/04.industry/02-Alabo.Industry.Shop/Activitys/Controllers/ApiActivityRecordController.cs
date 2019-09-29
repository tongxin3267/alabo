using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Industry.Shop.Activitys.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/ActivityRecord/[action]")]
    public class ApiActivityRecordController : ApiBaseController<ActivityRecord, long>
    {
        public ApiActivityRecordController()
        {
            BaseService = Resolve<IActivityRecordService>();
        }
    }
}