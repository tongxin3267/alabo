using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
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
        [Display(Description = "��̨�������")]
        public ApiResult<PagedList<TaskQueue>> TaskQueueList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<ITaskQueueService>().GetPageList(Query);
            return ApiResult.Success(model);
        }
    }
}