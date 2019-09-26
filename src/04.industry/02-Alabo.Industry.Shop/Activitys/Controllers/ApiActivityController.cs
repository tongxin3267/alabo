using System.ComponentModel.DataAnnotations;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Industry.Shop.Activitys.Dtos;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Activitys.Controllers
{
    /// <summary>
    ///     api activity
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Activity/[action]")]
    public class ApiActivityController : ApiBaseController<Activity, long>
    {
        /// <summary>
        ///     ��ȡ�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡ�")]
        public ApiResult<ActivityEditOutput> GetView([FromQuery] ActivityEditInput input)
        {
            if (!this.IsFormValid()) return ApiResult.Failure<ActivityEditOutput>(this.FormInvalidReason());

            var model = Resolve<IActivityApiService>().GetView(input);
            return ApiResult.Success(model);
        }

        /// <summary>
        ///     ����
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����")]
        public ApiResult Save([FromBody] ActivityEditOutput input)
        {
            if (!this.IsFormValid()) return ApiResult.Failure<ActivityEditOutput>(this.FormInvalidReason());
            var result = Resolve<IActivityApiService>().Save(input);
            return ToResult(result);
        }
    }
}