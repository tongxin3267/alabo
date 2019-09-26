using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;

using Alabo.App.Core.User;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Order.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Cart/[action]")]
    public class ApiCartController : ApiBaseController<Cart, ObjectId> {

        public ApiCartController() : base() {
            BaseService = Resolve<ICartService>();
        }

        /// <summary>
        ///     添加商品到购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "添加商品到购物车")]
        [ApiAuth]
        public ApiResult AddCart([FromBody] OrderProductInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ICartService>().AddCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     获取购物车数据
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "获取购物车数据")]
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
        ///     删除购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "删除购物车")]
        [ApiAuth]
        public ApiResult RemoveCart([FromQuery] OrderProductInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<ICartService>().RemoveCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     更新购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPut]
        [Display(Description = "更新购物车")]
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