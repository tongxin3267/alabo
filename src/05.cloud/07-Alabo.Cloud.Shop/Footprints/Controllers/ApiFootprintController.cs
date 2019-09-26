using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Share.Attach.Domain.Dtos;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Share.Attach.Domain.Enums;
using Alabo.App.Share.Attach.Domain.Services;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.Domains.Enums;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Alabo.App.Open.Attach.Domain.Enums;

namespace Alabo.App.Share.Attach.Controllers {
    //2019年9月24日 重构注释
    ///// <summary>
    /////
    ///// </summary>
    //[ApiExceptionFilter]
    //[Route("Api/Footprint/[action]")]
    //public class ApiFootprintController : ApiBaseController<Footprint, ObjectId> {
    //    /// <summary>
    //    ///
    //    /// </summary>
    //    public ApiFootprintController() : base() {
    //        BaseService = Resolve<IFootprintService>();
    //    }

    //    /// <summary>
    //    ///    足迹列表
    //    /// </summary>
    //    [HttpGet]
    //    [Display(Description = "足迹列表")]
    //    public ApiResult List([FromQuery]long loginUserId) {
    //        var result = Resolve<IFootprintService>().GetList(u => u.UserId == loginUserId);
    //        return ApiResult.Success(result);
    //    }

    //    /// <summary>
    //    ///    添加足迹
    //    /// </summary>
    //    [HttpGet]
    //    [Display(Description = "添加足迹")]
    //    public ApiResult Add([FromQuery] FootprintInput parameter) {
    //        var result = Resolve<IFootprintService>().Add(parameter);
    //        return ToResult(result);
    //    }

    //    /// <summary>
    //    /// 清空足迹
    //    /// </summary>
    //    [HttpGet]
    //    public ApiResult Clear([FromQuery] FootprintType type, long loginUserId) {
    //        return null;
    //    }
    //}
}