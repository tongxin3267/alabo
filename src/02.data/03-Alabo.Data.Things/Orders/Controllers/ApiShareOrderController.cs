using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Data.Things.Orders.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.RestfulApi;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.Things.Orders.Controllers
{
    /// <summary>
    ///     分润订单相关接口
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/ShareOrder/[action]")]
    public class ShareOrderApiController : ApiBaseController<ShareOrder, long>
    {
        public ShareOrderApiController(RestClientConfig restClientConfig
        )
        {
            BaseService = Resolve<IShareOrderService>();
        }

        /// <summary>
        ///     查询当个订单的详细信息,包括任务执行进度、状态等
        /// </summary>
        /// <param name="id">分润订单Id</param>
        [HttpGet]
        [Display(Description = "查询单个订单的详细信息，包括任务执行进度、状态等")]
        public ApiResult<ShareOrder> GetSingle(long id)
        {
            var result = Resolve<IShareOrderService>().GetSingle(id);
            if (result == null) return ApiResult.Failure<ShareOrder>("分润订单信息不存在！");

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     查询分润订单列表，根据条件查询分润订单列表
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "查询分润订单列表，根据条件查询分润订单列表")]
        [ApiAuth]
        public ApiResult<PagedList<ShareOrder>> GetList([FromQuery] ApiBaseInput parameter)
        {
            //接口还未调试，以及参数
            var pageList = Resolve<IShareOrderService>().GetPagedList(parameter.ToJson());
            return ApiResult.Success(pageList);
        }
    }
}