using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Order.Controllers {

    [ApiExceptionFilter]
    [Route("Api/OrderDelivery/[action]")]
    public class ApiOrderDeliveryController : ApiBaseController<OrderDelivery, long> {

        public ApiOrderDeliveryController() : base() {
            BaseService = Resolve<IOrderDeliveryService>();
        }


        [HttpGet]
        [Display(Description = "商家数据")]
        public ApiResult<PagedList<ViewOrderDeliveryList>> OrderDeliveryList([FromQuery] PagedInputDto parameter) {
            var model = Resolve<IOrderDeliveryService>().GetPageList(Query);
            return ApiResult.Success(model);
        }
    }
}