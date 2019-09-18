using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Product.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/ProductSku/[action]")]
    public class ApiProductSkuController : ApiBaseController<ProductSku, long>
    {

        public ApiProductSkuController()
            : base() {
            BaseService = Resolve<IProductSkuService>();
        }

        /// <summary>
        /// 获取会员等级价格
        /// </summary>
        [HttpGet]
        [Display(Description = "获取会员等级价格")]
        public ApiResult<List<ProductSku>> GetGradePrice([FromQuery] long productId)
        {
            var result = Resolve<IProductSkuService>().GetGradePrice(productId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 更新会员等级价格
        /// </summary>
        [HttpPost]
        [Display(Description = "更新会员等级价格")]
        public ApiResult UpdateGradePrice([FromBody] List<ProductSku> productSkus)
        {
            var result = Resolve<IProductSkuService>().UpdateGradePrice(productSkus);
            return result;
        }
    }
}