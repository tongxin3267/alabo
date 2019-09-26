using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Market.TeamIntro.Domain.Services;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Market.TeamIntro.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Team/[action]")]
    public class ApiTeamIntroController : ApiBaseController<Domain.Entities.TeamIntro, ObjectId> {

        public ApiTeamIntroController() : base() {
            BaseService = Resolve<ITeamIntroService>();
        }

        /// <summary>
        /// 获取团队成员列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取团队成员列表")]
        public ApiResult<IList<Domain.Entities.TeamIntro>> List()
        {

            try
            {
                var apiService = Resolve<IApiService>();

                var ret = Resolve<ITeamIntroService>().GetList();

                ret.Foreach(teamintro => teamintro.Image = apiService.ApiImageUrl(teamintro.Image));

                return ApiResult.Success(ret);
            }
            catch (Exception e)
            {
                return ApiResult.Failure<IList<Domain.Entities.TeamIntro>>(e.Message);
            }
        }
    }
}