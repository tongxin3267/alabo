using Alabo.Domains.Base.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Users.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Core.Logs.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Logs/[action]")]
    public class ApiLogsController : ApiBaseController<Domains.Base.Entities.Logs, ObjectId>
    {
        public ApiLogsController()
        {
            BaseService = Resolve<ILogsService>();
        }

        [HttpPost]
        [Display(Description = "添加日志")]
        public ApiResult Add([FromBody] LogInput logInput)
        {
            if (logInput != null && !logInput.Message.IsNullOrEmpty())
                Resolve<IAlaboUserService>().Log(logInput.Message);

            return ApiResult.Success();
        }
    }

    public class LogInput
    {
        public string Message { get; set; }
    }
}