using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
    [Route("Api/UpgradeRecord/[action]")]
    public class ApiUpgradeRecordController : ApiBaseController<UpgradeRecord, ObjectId> {

        public ApiUpgradeRecordController() : base() {
            BaseService = Resolve<IUpgradeRecordService>();
        }

        [HttpGet]
        [Display(Description = "Éý¼¶¼ÇÂ¼")]
        public ApiResult<PagedList<UpgradeRecord>> UpgradeList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IUpgradeRecordService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }
    }
}