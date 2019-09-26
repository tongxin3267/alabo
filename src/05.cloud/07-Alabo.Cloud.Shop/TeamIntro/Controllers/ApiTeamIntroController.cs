using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.TeamIntro.Domain.Services;
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
        /// ��ȡ�Ŷӳ�Ա�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡ�Ŷӳ�Ա�б�")]
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