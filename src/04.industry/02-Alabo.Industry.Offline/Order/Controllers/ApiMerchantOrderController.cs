using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Offline.Order.Domain.Dtos;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.Domain.Services;
using Alabo.Industry.Offline.Order.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Offline.Order.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantOrder/[action]")]
    public class ApiMerchantOrderController : ApiBaseController<MerchantOrder, long>
    {
        public ApiMerchantOrderController()
        {
            BaseService = Resolve<IMerchantOrderService>();
        }

        /// <summary>
        ///     确认订单信息
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "确认订单信息")]
        [ApiAuth]
        public ApiResult<MerchantCartOutput> BuyInfo([FromBody] MerchantBuyInfoInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<MerchantCartOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);

            var serviceResult = Resolve<IMerchantOrderService>().BuyInfo(parameter);
            if (!serviceResult.Item1.Succeeded)
                return ApiResult.Failure<MerchantCartOutput>(serviceResult.Item1.ToString(),
                    MessageCodes.ServiceFailure);

            return ApiResult.Success(serviceResult.Item2);
        }

        /// <summary>
        ///     提交订单
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "提交订单")]
        [ApiAuth]
        public ApiResult<MerchantOrderBuyOutput> Buy([FromBody] MerchantOrderBuyInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<MerchantOrderBuyOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);

            var result = Resolve<IMerchantOrderService>().Buy(parameter);
            if (!result.Item1.Succeeded)
                return ApiResult.Failure<MerchantOrderBuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     订单列表
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "订单列表")]
        [ApiAuth]
        public ApiResult<PagedList<MerchantOrderList>> BuyOrderList([FromQuery] MerchantOrderListInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure<PagedList<MerchantOrderList>>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            var result = Resolve<IMerchantOrderService>().GetOrderList(parameter);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     订单详情
        /// </summary>
        [HttpGet]
        [Display(Description = "订单详情")]
        [ApiAuth]
        public ApiResult GetOrder(long id)
        {
            var result = Resolve<IMerchantOrderService>().GetOrderSingle(id);
            if (!result.Item1.Succeeded) return ApiResult.Failure(result.Item1.ErrorMessages);

            return ApiResult.Success(result.Item2);
        }
    }
}