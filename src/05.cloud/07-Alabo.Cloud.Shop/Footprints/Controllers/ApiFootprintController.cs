namespace Alabo.Cloud.Shop.Footprints.Controllers {
    //2019��9��24�� �ع�ע��
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
    //    ///    �㼣�б�
    //    /// </summary>
    //    [HttpGet]
    //    [Display(Description = "�㼣�б�")]
    //    public ApiResult List([FromQuery]long loginUserId) {
    //        var result = Resolve<IFootprintService>().GetList(u => u.UserId == loginUserId);
    //        return ApiResult.Success(result);
    //    }

    //    /// <summary>
    //    ///    ����㼣
    //    /// </summary>
    //    [HttpGet]
    //    [Display(Description = "����㼣")]
    //    public ApiResult Add([FromQuery] FootprintInput parameter) {
    //        var result = Resolve<IFootprintService>().Add(parameter);
    //        return ToResult(result);
    //    }

    //    /// <summary>
    //    /// ����㼣
    //    /// </summary>
    //    [HttpGet]
    //    public ApiResult Clear([FromQuery] FootprintType type, long loginUserId) {
    //        return null;
    //    }
    //}
}