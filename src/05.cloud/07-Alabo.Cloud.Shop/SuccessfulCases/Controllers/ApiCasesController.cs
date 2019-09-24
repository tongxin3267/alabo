using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.App.Market.SuccessfulCases.Domains.Services;
using Alabo.App.Market.TeamIntro.Domain.Services;
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
        /// ��ȡ�ɹ������б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡ�ɹ������б�")]
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