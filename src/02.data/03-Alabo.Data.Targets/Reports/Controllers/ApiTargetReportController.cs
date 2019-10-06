using Alabo.Data.Targets.Reports.Domain.Entities;
using Alabo.Data.Targets.Reports.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Alabo.Data.Targets.Reports.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/TargetReport/[action]")]
    public class ApiTargetReportController : ApiBaseController<TargetReport, long>
    {
        public ApiTargetReportController() : base() {
            BaseService = Resolve<ITargetReportService>();
        }
    }
}