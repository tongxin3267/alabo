using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.User.Controllers {

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