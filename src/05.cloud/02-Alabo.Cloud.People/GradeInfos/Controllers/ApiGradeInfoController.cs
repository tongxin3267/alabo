using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.People.GradeInfos.Domain.Entities;
using Alabo.Cloud.People.GradeInfos.Domain.Servcies;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.GradeInfos.Controllers {

    [ApiExceptionFilter]
    [Route("Api/GradeInfo/[action]")]
    public class ApiGradeInfoController : ApiBaseController<GradeInfo, ObjectId> {

        public ApiGradeInfoController() : base() {
            BaseService = Resolve<IGradeInfoService>();
        }

        [HttpGet]
        [Display(Description = "商家数据")]
        public ApiResult<PagedList<GradeInfo>> GradeInfoList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IGradeInfoService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }
    }
}