using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.App.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.App.Shop.Activitys.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Controllers
{

    /// <summary>
    /// api activity
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Activity/[action]")]
    public class ApiActivityController : ApiBaseController<Activity, long>
    {

        /// <summary>
        /// constructor
        /// </summary>
        public ApiActivityController()
            : base()
        {
        }

        /// <summary>
        /// 获取活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取活动")]
        public ApiResult<ActivityEditOutput> GetView([FromQuery]ActivityEditInput input)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<ActivityEditOutput>(this.FormInvalidReason());
            }
   
            var model = Resolve<IActivityApiService>().GetView(input);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// 保存活动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "保存活动")]
        public ApiResult Save([FromBody]ActivityEditOutput input)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<ActivityEditOutput>(this.FormInvalidReason());
            }
            var result = Resolve<IActivityApiService>().Save(input);
            return ToResult(result);
        }
    }
}