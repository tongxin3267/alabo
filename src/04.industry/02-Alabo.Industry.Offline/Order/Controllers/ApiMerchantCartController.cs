using System.ComponentModel.DataAnnotations;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Offline.Order.Domain.Dtos;
using Alabo.Industry.Offline.Order.Domain.Entities;
using Alabo.Industry.Offline.Order.Domain.Services;
using Alabo.Industry.Offline.Product.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Offline.Order.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantCart/[action]")]
    public class ApiMerchantCartController : ApiBaseController<MerchantCart, ObjectId>
    {
        /// <summary>
        ///     添加商品到购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "添加商品到购物车")]
        [ApiAuth]
        public ApiResult AddCart([FromBody] MerchantCartInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var stokCount = 0L;
            var merchantModel = Resolve<IMerchantProductService>().GetSingle(parameter.MerchantProductId);
            if (merchantModel != null) {
                merchantModel.Skus.ForEach(p =>
                {
                    if (p.SkuId == parameter.SkuId) {
                        stokCount = p.Stock;
                    }
                });
            }

            var serviceResult = Resolve<IMerchantCartService>().AddCart(parameter);
            if (serviceResult.Succeeded) {
                serviceResult.ReturnObject = stokCount;
            }

            return ToResult(serviceResult);
        }

        /// <summary>
        ///     获取购物车数据
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "获取购物车数据")]
        [ApiAuth]
        public ApiResult<MerchantCartOutput> GetCart([FromQuery] long loginUserId, long userId, string merchantStoreId)
        {
            var result = Resolve<IMerchantCartService>().GetCart(userId > 0 ? userId : loginUserId, merchantStoreId);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<MerchantCartOutput>(result.Item1.ToString());
        }

        /// <summary>
        ///     删除购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "删除购物车")]
        [ApiAuth]
        public ApiResult RemoveCart([FromQuery] MerchantCartInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IMerchantCartService>().RemoveCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     更新购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPut]
        [Display(Description = "更新购物车")]
        [ApiAuth]
        public ApiResult UpdateCart([FromBody] MerchantCartInput parameter)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var stokCount = 0L;
            var merchantModel = Resolve<IMerchantProductService>().GetSingle(parameter.MerchantProductId);
            if (merchantModel != null) {
                merchantModel.Skus.ForEach(p =>
                {
                    if (p.SkuId == parameter.SkuId) {
                        stokCount = p.Stock;
                    }
                });
            }

            var serviceResult = Resolve<IMerchantCartService>().UpdateCart(parameter);
            serviceResult.ReturnObject = stokCount;

            return ToResult(serviceResult);
        }
    }
}