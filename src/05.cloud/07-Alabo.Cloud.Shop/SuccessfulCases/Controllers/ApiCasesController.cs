using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.App.Market.SuccessfulCases.Domains.Services;
using Alabo.App.Market.TeamIntro.Domain.Services;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Market.SuccessfulCases.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Cases/[action]")]
    public class ApiCasesController : ApiBaseController<Cases, ObjectId> {

        public ApiCasesController() : base() {
            BaseService = Resolve<ICasesService>();
        }

        /// <summary>
        /// 获取成功案例列表
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