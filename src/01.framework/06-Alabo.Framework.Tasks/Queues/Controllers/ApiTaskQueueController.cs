using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Tasks.Controllers {

    [ApiExceptionFilter]
    [Route("Api/TaskQueue/[action]")]
    public class ApiTaskQueueController : ApiBaseController<TaskQueue, long> {

        public ApiTaskQueueController() : base() {
            BaseService = Resolve<ITaskQueueService>();
        }

        [HttpGet]
        [Display(Description = "后台任务队列")]
        public ApiResult<PagedList<TaskQueue>> TaskQueueList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<ITaskQueueService>().GetPageList(Query);
            return ApiResult.Success(model);
        }
    }
}