using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.Shop.TeamIntro.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Shop.TeamIntro.Controllers {

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