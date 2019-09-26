using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.School.Materials.Domain.Entities;
using Alabo.Cloud.School.Materials.Domain.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.School.Materials.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Material/[action]")]
    public class ApiMaterialController : ApiBaseController<Material, ObjectId>
    {
        public ApiMaterialController()
        {
            BaseService = Resolve<IMaterialService>();
        }

        /// <summary>
        ///     获取宣传材料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取宣传材料列表")]
        public ApiResult<IList<Material>> List()
        {
            try
            {
                var apiService = Resolve<IApiService>();

                var ret = Resolve<IMaterialService>().GetList();

                ret.Foreach(material => material.Image = apiService.ApiImageUrl(material.Image));

                return ApiResult.Success(ret);
            }
            catch (Exception e)
            {
                return ApiResult.Failure<IList<Material>>(e.Message);
            }
        }
    }
}