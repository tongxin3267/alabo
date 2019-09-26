using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Market.PromotionalMaterial.Domain.Entities;
using Alabo.App.Market.PromotionalMaterial.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Market.PromotionalMaterial.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Material/[action]")]
    public class ApiMaterialController : ApiBaseController<Material, ObjectId> {

        public ApiMaterialController() : base() {
            BaseService = Resolve<IMaterialService>();
        }

        /// <summary>
        /// 获取宣传材料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取宣传材料列表")]
        public ApiResult<IList<Material>> List() {
            try {
                var apiService = Resolve<IApiService>();

                var ret = Resolve<IMaterialService>().GetList();

                ret.Foreach(material => material.Image = apiService.ApiImageUrl(material.Image));

                return ApiResult.Success(ret);
            } catch (Exception e) {
                return ApiResult.Failure<IList<Material>>(e.Message);
            }
        }
    }
}