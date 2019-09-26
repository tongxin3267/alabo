using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Market.SuccessfulCases.Domains.Entities;
using Alabo.App.Market.SuccessfulCases.Domains.Services;
using Alabo.Extensions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

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
        public ApiResult<IList<Cases>> List() {
            try {
                var apiService = Resolve<IApiService>();

                var ret = Resolve<ICasesService>().GetList();

                ret.Foreach(cases => cases.Image = apiService.ApiImageUrl(cases.Image));

                return ApiResult.Success(ret);
            } catch (Exception e) {
                return ApiResult.Failure<IList<Cases>>(e.Message);
            }
        }
    }
}