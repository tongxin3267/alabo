using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Modules.PreSells.Controller
{
    /// <summary>
    /// 预售API
    /// </summary>
    [ApiExceptionFilter, Route("Api/PreSells/[action]")]
    public class PreSellsApiController : ApiBaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        public PreSellsApiController() : base()
        {
        }

        /// <summary>
        /// PreSellsList
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ApiResult<List<String>> PreSellsList([FromQuery] long productId)
        {
            return ApiResult.Success(new List<String>() { "T1", "T2" });
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
