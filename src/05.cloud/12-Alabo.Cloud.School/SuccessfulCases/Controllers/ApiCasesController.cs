using Alabo.Cloud.School.SuccessfulCases.Domains.Entities;
using Alabo.Cloud.School.SuccessfulCases.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.School.SuccessfulCases.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Cases/[action]")]
    public class ApiCasesController : ApiBaseController<Cases, ObjectId>
    {
        public ApiCasesController()
        {
            BaseService = Resolve<ICasesService>();
        }

        /// <summary>
        ///     获取成功案例列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取成功案例列表")]
        public ApiResult<IList<Cases>> List()
        {
            try
            {
                var apiService = Resolve<IApiService>();

                var ret = Resolve<ICasesService>().GetList();

                ret.Foreach(cases => cases.Image = apiService.ApiImageUrl(cases.Image));

                return ApiResult.Success(ret);
            }
            catch (Exception e)
            {
                return ApiResult.Failure<IList<Cases>>(e.Message);
            }
        }
    }
}