using System.ComponentModel.DataAnnotations;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using Alabo.Industry.Shop.Carts.Domain.Services;
using Alabo.Industry.Shop.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Carts.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Cart/[action]")]
    public class ApiCartController : ApiBaseController<Cart, ObjectId> {

        public ApiCartController() : base() {
            BaseService = Resolve<ICartService>();
        }

        /// <summary>
        ///     �����Ʒ�����ﳵ
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "�����Ʒ�����ﳵ")]
        [ApiAuth]
        public ApiResult AddCart([FromBody] OrderProductInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ICartService>().AddCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     ��ȡ���ﳵ����
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "��ȡ���ﳵ����")]
        [ApiAuth]
        public ApiResult<StoreProductSku> GetCart([FromQuery] long loginUserId) {
            var result = Resolve<ICartService>().GetCart(loginUserId);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }
            var storeProductSku = new StoreProductSku();
            return ApiResult.Success(storeProductSku);
            //return ApiResult.Failure<StoreProductSku>(result.Item1.ToString(), MessageCodes.ParameterValidationFailure);
        }

        /// <summary>
        ///     ɾ�����ﳵ
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "ɾ�����ﳵ")]
        [ApiAuth]
        public ApiResult RemoveCart([FromQuery] OrderProductInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ICartService>().RemoveCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     ���¹��ﳵ
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPut]
        [Display(Description = "���¹��ﳵ")]
        [ApiAuth]
        public ApiResult UpdateCart([FromBody] OrderProductInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ICartService>().UpdateCart(parameter);
            return ToResult(serviceResult);
        }
    }
}