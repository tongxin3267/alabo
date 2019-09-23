using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Share.Attach.Domain.Dtos;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Share.Attach.Domain.Enums;
using Alabo.App.Share.Attach.Domain.Services;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.Domains.Enums;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Share.Attach.Controllers {

    /// <summary>
    ///
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Footprint/[action]")]
    public class ApiFootprintController : ApiBaseController<Footprint, ObjectId> {

        /// <summary>
        ///
        /// </summary>
        public ApiFootprintController() : base() {
            BaseService = Resolve<IFootprintService>();
        }

        /// <summary>
        ///    ×ã¼£ÁÐ±í
        /// </summary>
        [HttpGet]
        [Display(Description = "×ã¼£ÁÐ±í")]
        public ApiResult List([FromQuery]long loginUserId) {
            var result = Resolve<IFootprintService>().GetList(u => u.UserId == loginUserId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///    Ìí¼Ó×ã¼£
        /// </summary>
        [HttpGet]
        [Display(Description = "Ìí¼Ó×ã¼£")]
        public ApiResult Add([FromQuery] FootprintInput parameter) {
            var result = Resolve<IFootprintService>().Add(parameter);
            return ToResult(result);
        }

        /// <summary>
        /// Çå¿Õ×ã¼£
        /// </summary>
        [HttpGet]
        public ApiResult Clear([FromQuery] FootprintType type, long loginUserId) {
            return null;
        }
    }
}