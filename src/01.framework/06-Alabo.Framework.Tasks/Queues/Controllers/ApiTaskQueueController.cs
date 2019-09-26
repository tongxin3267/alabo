using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Tasks.Queues.Controllers {

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