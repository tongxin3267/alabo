using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Offline.Order.Domain.Dtos;
using Alabo.App.Offline.Order.Domain.Entities;
using Alabo.App.Offline.Order.Domain.Services;
using Alabo.App.Offline.Product.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Order.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantCart/[action]")]
    public class ApiMerchantCartController : ApiBaseController<MerchantCart, ObjectId>
    {
        public ApiMerchantCartController() : base()
        {
        }

        /// <summary>
        /// 添加商品到购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "添加商品到购物车")]
        [ApiAuth]
        public ApiResult AddCart([FromBody] MerchantCartInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var stokCount = 0L;
            var merchantModel = Resolve<IMerchantProductService>().GetSingle(parameter.MerchantProductId);
            if (merchantModel != null)
            {
                merchantModel.Skus.ForEach(p =>
                {
                    if (p.SkuId == parameter.SkuId)
                    {
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
        /// 获取购物车数据
        /// </summary>
        /// <param name="loginUserId"></param>
        [HttpGet]
        [Display(Description = "获取购物车数据")]
        [ApiAuth]
        public ApiResult<MerchantCartOutput> GetCart([FromQuery] long loginUserId, long userId, string merchantStoreId)
        {
            var result = Resolve<IMerchantCartService>().GetCart(userId > 0 ? userId : loginUserId, merchantStoreId);
            if (result.Item1.Succeeded)
            {
                return ApiResult.Success(result.Item2);
            }
            return ApiResult.Failure<MerchantCartOutput>(result.Item1.ToString());
        }

        /// <summary>
        /// 删除购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "删除购物车")]
        [ApiAuth]
        public ApiResult RemoveCart([FromQuery] MerchantCartInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IMerchantCartService>().RemoveCart(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        /// 更新购物车
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPut]
        [Display(Description = "更新购物车")]
        [ApiAuth]
        public ApiResult UpdateCart([FromBody] MerchantCartInput parameter)
        {
            if (!this.IsFormValid())
            {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var stokCount = 0L;
            var merchantModel = Resolve<IMerchantProductService>().GetSingle(parameter.MerchantProductId);
            if (merchantModel != null)
            {
                merchantModel.Skus.ForEach(p =>
                {
                    if (p.SkuId == parameter.SkuId)
                    {
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