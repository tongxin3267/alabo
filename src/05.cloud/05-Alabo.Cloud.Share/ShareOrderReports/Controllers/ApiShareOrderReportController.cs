using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Entities;
using _05_Alabo.Cloud.Share.ShareOrderReports.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace _05_Alabo.Cloud.Share.ShareOrderReports.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ShareOrderReport/[action]")]
    public class ApiShareOrderReportController : ApiBaseController<ShareOrderReport, ObjectId> {

        public ApiShareOrderReportController() : base() {
            BaseService = Resolve<IShareOrderReportService>();
        }
    }
}