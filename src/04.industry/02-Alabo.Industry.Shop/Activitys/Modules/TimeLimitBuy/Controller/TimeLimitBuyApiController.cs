﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Activitys.Modules.TimeLimitBuy.Controller
{
    /// <summary>
    ///     预售API
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/TimeLimitBuy/[action]")]
    public class TimeLimitBuyApiController : ApiBaseController
    {
        /// <summary>
        ///     PreSellsList
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ApiResult<List<string>> T1([FromQuery] long productId)
        {
            return ApiResult.Success(new List<string> {"T1", "T2"});
        }

        /// <summary>
        ///     商品列表，对应zk-product-item组件
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public ApiResult<ProductItemApiOutput> List([FromQuery] ProductApiInput parameter)
        {
            var apiOutput = Resolve<IProductService>().GetProductItems(parameter);
            return ApiResult.Success(apiOutput);
        }
    }
}