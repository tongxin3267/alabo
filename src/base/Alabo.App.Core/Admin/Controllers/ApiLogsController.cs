using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

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
                Resolve<IUserService>().Log(logInput.Message);
            }

            return ApiResult.Success();
        }
    }

    public class LogInput {
        public string Message { get; set; }
    }
}