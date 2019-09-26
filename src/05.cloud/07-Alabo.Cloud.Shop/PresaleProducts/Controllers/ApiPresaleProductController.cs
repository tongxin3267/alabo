using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Entities;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.Shop.PresaleProducts.Controllers {

    [ApiExceptionFilter]
    [Route("Api/PresaleProduct/[action]")]
    public class ApiPresaleProductController : ApiBaseController<PresaleProduct, ObjectId> {

        /// <summary>
        /// 构造
        /// </summary>
        public ApiPresaleProductController()
            : base() {
            BaseService = Resolve<IPresaleProductService>();
        }

        /// <summary>
        /// 预售商品列表接口
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "预售商品列表接口")]
        public ApiResult<ProductItemApiOutput> List([FromQuery] ProductApiInput parameter) {
            var apiOutput = Resolve<IPresaleProductService>().GetProducts(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 预售区商品列表接口
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "预售区商品列表接口")]
        public ApiResult<PresaleProductItemApiOutput> PresaleAreaList([FromQuery] PresaleProductApiInput parameter) {
            var apiOutput = Resolve<IPresaleProductService>().GetPresaleProducts(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 添加预售商品接口
        /// </summary>
        [HttpPost]
        [Display(Description = "添加预售商品接口")]
        public ApiResult Add([FromBody] PresaleProductEdit presaleProduct) {
            return AddMany(new List<PresaleProductEdit> { presaleProduct });
        }

        /// <summary>
        /// 批量添加预售商品接口
        /// </summary>
        [HttpPost]
        [Display(Description = "批量添加预售商品接口")]
        public ApiResult AddMany([FromBody] IList<PresaleProductEdit> presaleProducts) {
            var result = Resolve<IPresaleProductService>().AddPresaleProducts(presaleProducts);
            return ToResult(result);
        }

        /// <summary>
        /// 更新预售商品接口
        /// </summary>
        [HttpPost]
        [Display(Description = "更新预售商品接口")]
        public ApiResult Update([FromBody] PresaleProductEdit presaleProduct) {
            var result = Resolve<IPresaleProductService>().UpdatePresaleProduct(presaleProduct);
            return ToResult(result);
        }

        /// <summary>
        /// 更新预售商品状态接口
        /// </summary>
        [HttpGet]
        [Display(Description = "更新预售商品状态接口")]
        public ApiResult UpdateStatus([FromQuery] long id, [FromQuery]ProductStatus status) {
            var result = Resolve<IPresaleProductService>().UpdateStatus(id, status);
            return ToResult(result);
        }
    }
}