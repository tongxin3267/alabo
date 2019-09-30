using Alabo.App.Kpis.GradeKpis.Domain.Entities;
using Alabo.App.Kpis.GradeKpis.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Kpis.GradeKpis.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/GradeKpi/[action]")]
    public class ApiGradeKpiController : ApiBaseController<GradeKpi, ObjectId>
    {
        public ApiGradeKpiController()
        {
            BaseService = Resolve<IGradeKpiService>();
        }

        [HttpGet]
        [Display(Description = "µÈ¼¶¿¼ºË")]
        public ApiResult<PagedList<GradeKpi>> GradeKpiList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IGradeKpiService>().GetGradeKpiList(Query);
            return ApiResult.Success(model);
        }
    }
}