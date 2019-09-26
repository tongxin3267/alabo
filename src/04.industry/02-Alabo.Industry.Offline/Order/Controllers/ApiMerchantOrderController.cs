using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Offline.Order.Domain.Dtos;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.App.Offline.Order.Domain.Services;
using Alabo.App.Offline.Order.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Order.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantOrder/[action]")]
    public class ApiMerchantOrderController : ApiBaseController<MerchantOrder, long>
    {

        public ApiMerchantOrderController()
            : base()
        {
            BaseService = Resolve<IMerchantOrderService>();
        }

        /// <summary>
        /// ȷ�϶�����Ϣ
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "ȷ�϶�����Ϣ")]
        [ApiAuth]
        public ApiResult<MerchantCartOutput> BuyInfo([FromBody] MerchantBuyInfoInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<MerchantCartOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IMerchantOrderService>().BuyInfo(parameter);
            if (!serviceResult.Item1.Succeeded)
            {
                return ApiResult.Failure<MerchantCartOutput>(serviceResult.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(serviceResult.Item2);
        }

        /// <summary>
        /// �ύ����
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "�ύ����")]
        [ApiAuth]
        public ApiResult<MerchantOrderBuyOutput> Buy([FromBody] MerchantOrderBuyInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<MerchantOrderBuyOutput>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var result = Resolve<IMerchantOrderService>().Buy(parameter);
            if (!result.Item1.Succeeded)
            {
                return ApiResult.Failure<MerchantOrderBuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }

            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        /// �����б�
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "�����б�")]
        [ApiAuth]
        public ApiResult<PagedList<MerchantOrderList>> BuyOrderList([FromQuery] MerchantOrderListInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<PagedList<MerchantOrderList>>(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var result = Resolve<IMerchantOrderService>().GetOrderList(parameter);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ��������
        /// </summary>
        [HttpGet]
        [Display(Description = "��������")]
        [ApiAuth]
        public ApiResult GetOrder(long id)
        {
            var result = Resolve<IMerchantOrderService>().GetOrderSingle(id);
            if (!result.Item1.Succeeded)
            {
                return ApiResult.Failure(result.Item1.ErrorMessages);
            }

            return ApiResult.Success(result.Item2);
        }
    }
}