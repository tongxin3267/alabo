using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.App.Share.Kpi.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.Kpi.Controllers {

    [ApiExceptionFilter]
    [Route("Api/GradeKpi/[action]")]
    public class ApiGradeKpiController : ApiBaseController<GradeKpi, ObjectId> {

        public ApiGradeKpiController() : base() {
            BaseService = Resolve<IGradeKpiService>();
        }

        [HttpGet]
        [Display(Description = "µÈ¼¶¿¼ºË")]
        public ApiResult<PagedList<GradeKpi>> GradeKpiList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IGradeKpiService>().GetGradeKpiList(Query);
            return ApiResult.Success(model);
        }
    }
}