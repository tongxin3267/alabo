using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.OrderActions.Domain.Entities;
using Alabo.Industry.Shop.OrderActions.Domain.Services;
using Alabo.Industry.Shop.Orders.Domain.Enums;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.OrderActions.Controllers {

    [ApiExceptionFilter]
    [Route("Api/OrderAction/[action]")]
    public class ApiOrderActionController : ApiBaseController<OrderAction, long> {

        public ApiOrderActionController() : base() {
            BaseService = Resolve<IOrderActionService>();
        }

        /// <summary>
        /// 代付款订单支付
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "代付款订单支付")]
        [ApiAuth]
        public ApiResult<BuyOutput> Pay(long id, long loginUserId) {
            if (id <= 0) {
                return ApiResult.Failure<BuyOutput>("Id不正确");
            }
            var result = Resolve<IOrderBuyServcie>().Pay(id, loginUserId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<BuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }
            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     订单取消
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "订单取消")]
        [ApiAuth]
        public ApiResult Cancel(long id, long loginUserId) {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.UserId == loginUserId);
            if (order == null) {
                return ApiResult.Failure("订单不存在");
            }
            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("订单取消成功");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "获取订单操作记录")]
        [ApiAuth]
        public ApiResult GetOrderActionRecord(long orderId) {
            var orderAction = Resolve<IOrderActionService>().GetList(u => u.OrderId == orderId).ToList();
            return ApiResult.Success(orderAction);
        }
    }
}