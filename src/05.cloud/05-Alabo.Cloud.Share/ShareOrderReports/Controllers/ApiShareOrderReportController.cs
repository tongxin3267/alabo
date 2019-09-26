using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Services;

namespace Alabo.App.Core.Tasks.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ShareOrderReport/[action]")]
    public class ApiShareOrderReportController : ApiBaseController<ShareOrderReport, ObjectId> {

        public ApiShareOrderReportController() : base() {
            BaseService = Resolve<IShareOrderReportService>();
        }
    }
}