using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Entities;
using Alabo.Industry.Shop.OrderDeliveries.Domain.Services;
using Alabo.Industry.Shop.Orders.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.OrderDeliveries.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/OrderDelivery/[action]")]
    public class ApiOrderDeliveryController : ApiBaseController<OrderDelivery, long>
    {
        public ApiOrderDeliveryController()
        {
            BaseService = Resolve<IOrderDeliveryService>();
        }


        [HttpGet]
        [Display(Description = "商家数据")]
        public ApiResult<PagedList<ViewOrderDeliveryList>> OrderDeliveryList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IOrderDeliveryService>().GetPageList(Query);
            return ApiResult.Success(model);
        }
    }
}