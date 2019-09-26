using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Basic.Grades.Domain.Entities;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Tasks.Controllers
{

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