using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Cloud.Shop.Footprints.Domain.Enums;
using Alabo.Cloud.Shop.Footprints.Domain.Services;
using Alabo.Cloud.Shop.Footprints.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Shop.Footprints.Domain
{
    /// <summary>
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Footprint/[action]")]
    public class ApiFootprintController : ApiBaseController<Footprint, ObjectId>
    {
        /// <summary>
        /// </summary>
        public ApiFootprintController()
        {
            BaseService = Resolve<IFootprintService>();
        }

        /// <summary>
        ///     ×ã¼£ÁÐ±í
        /// </summary>
        [HttpGet]
        [Display(Description = "×ã¼£ÁÐ±í")]
        public ApiResult List([FromQuery] long loginUserId)
        {
            var result = Resolve<IFootprintService>().GetList(u => u.UserId == loginUserId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Ìí¼Ó×ã¼£
        /// </summary>
        [HttpGet]
        [Display(Description = "Ìí¼Ó×ã¼£")]
        public ApiResult Add([FromQuery] FootprintInput parameter)
        {
            var result = Resolve<IFootprintService>().Add(parameter);
            return ToResult(result);
        }

        /// <summary>
        ///     Çå¿Õ×ã¼£
        /// </summary>
        [HttpGet]
        public ApiResult Clear([FromQuery] FootprintType type, long loginUserId)
        {
            return null;
        }
    }
}