using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Products.Controllers
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
        /// ��ȡ��Ա�ȼ��۸�
        /// </summary>
        [HttpGet]
        [Display(Description = "��ȡ��Ա�ȼ��۸�")]
        public ApiResult<List<ProductSku>> GetGradePrice([FromQuery] long productId)
        {
            var result = Resolve<IProductSkuService>().GetGradePrice(productId);
            return ApiResult.Success(result);
        }

        /// <summary>
        /// ���»�Ա�ȼ��۸�
        /// </summary>
        [HttpPost]
        [Display(Description = "���»�Ա�ȼ��۸�")]
        public ApiResult UpdateGradePrice([FromBody] List<ProductSku> productSkus)
        {
            var result = Resolve<IProductSkuService>().UpdateGradePrice(productSkus);
            return result;
        }
    }
}