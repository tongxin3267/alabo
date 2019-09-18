using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Order.Controllers {

    [ApiExceptionFilter]
    [Route("Api/OrderAction/[action]")]
    public class ApiOrderActionController : ApiBaseController<OrderAction, long> {

        public ApiOrderActionController() : base() {
            BaseService = Resolve<IOrderActionService>();
        }

        /// <summary>
        /// �������֧��
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "�������֧��")]
        [ApiAuth]
        public ApiResult<BuyOutput> Pay(long id, long loginUserId) {
            if (id <= 0) {
                return ApiResult.Failure<BuyOutput>("Id����ȷ");
            }
            var result = Resolve<IOrderBuyServcie>().Pay(id, loginUserId);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<BuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }
            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     ����ȡ��
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "����ȡ��")]
        [ApiAuth]
        public ApiResult Cancel(long id, long loginUserId) {
            var order = Resolve<IOrderService>().GetSingle(e => e.Id == id && e.UserId == loginUserId);
            if (order == null) {
                return ApiResult.Failure("����������");
            }
            order.OrderStatus = OrderStatus.Closed;
            Resolve<IOrderService>().Update(order);
            return ApiResult.Success("����ȡ���ɹ�");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡ����������¼")]
        [ApiAuth]
        public ApiResult GetOrderActionRecord(long orderId) {
            var orderAction = Resolve<IOrderActionService>().GetList(u => u.OrderId == orderId).ToList();
            return ApiResult.Success(orderAction);
        }
    }
}