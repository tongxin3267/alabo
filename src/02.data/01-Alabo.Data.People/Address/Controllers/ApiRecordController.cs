using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;

namespace Alabo.App.Core.Common.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Record/[action]")]
    public class ApiRecordController : ApiBaseController<Record, long> {

        public ApiRecordController() : base() {
            BaseService = Resolve<IRecordService>();
        }
    }
}