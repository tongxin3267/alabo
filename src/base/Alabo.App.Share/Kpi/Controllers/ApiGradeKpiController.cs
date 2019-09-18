using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Open.Kpi.Domain.Entities;
using Alabo.App.Open.Kpi.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Open.Kpi.Controllers {

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