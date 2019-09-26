using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Users.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Admin.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Logs/[action]")]
    public class ApiLogsController : ApiBaseController<Logs, ObjectId> {

        public ApiLogsController() : base() {
            BaseService = Resolve<ILogsService>();
        }

        [HttpPost]
        [Display(Description = "添加日志")]
        public ApiResult Add([FromBody]LogInput logInput) {
            if (logInput != null && !logInput.Message.IsNullOrEmpty()) {
                Resolve<IAlaboUserService>().Log(logInput.Message);
            }

            return ApiResult.Success();
        }
    }

    public class LogInput {
        public string Message { get; set; }
    }
}