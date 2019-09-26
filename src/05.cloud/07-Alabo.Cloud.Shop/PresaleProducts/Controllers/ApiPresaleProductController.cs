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
        /// ����
        /// </summary>
        public ApiPresaleProductController()
            : base() {
            BaseService = Resolve<IPresaleProductService>();
        }

        /// <summary>
        /// Ԥ����Ʒ�б�ӿ�
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "Ԥ����Ʒ�б�ӿ�")]
        public ApiResult<ProductItemApiOutput> List([FromQuery] ProductApiInput parameter) {
            var apiOutput = Resolve<IPresaleProductService>().GetProducts(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// Ԥ������Ʒ�б�ӿ�
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "Ԥ������Ʒ�б�ӿ�")]
        public ApiResult<PresaleProductItemApiOutput> PresaleAreaList([FromQuery] PresaleProductApiInput parameter) {
            var apiOutput = Resolve<IPresaleProductService>().GetPresaleProducts(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// ���Ԥ����Ʒ�ӿ�
        /// </summary>
        [HttpPost]
        [Display(Description = "���Ԥ����Ʒ�ӿ�")]
        public ApiResult Add([FromBody] PresaleProductEdit presaleProduct) {
            return AddMany(new List<PresaleProductEdit> { presaleProduct });
        }

        /// <summary>
        /// �������Ԥ����Ʒ�ӿ�
        /// </summary>
        [HttpPost]
        [Display(Description = "�������Ԥ����Ʒ�ӿ�")]
        public ApiResult AddMany([FromBody] IList<PresaleProductEdit> presaleProducts) {
            var result = Resolve<IPresaleProductService>().AddPresaleProducts(presaleProducts);
            return ToResult(result);
        }

        /// <summary>
        /// ����Ԥ����Ʒ�ӿ�
        /// </summary>
        [HttpPost]
        [Display(Description = "����Ԥ����Ʒ�ӿ�")]
        public ApiResult Update([FromBody] PresaleProductEdit presaleProduct) {
            var result = Resolve<IPresaleProductService>().UpdatePresaleProduct(presaleProduct);
            return ToResult(result);
        }

        /// <summary>
        /// ����Ԥ����Ʒ״̬�ӿ�
        /// </summary>
        [HttpGet]
        [Display(Description = "����Ԥ����Ʒ״̬�ӿ�")]
        public ApiResult UpdateStatus([FromQuery] long id, [FromQuery]ProductStatus status) {
            var result = Resolve<IPresaleProductService>().UpdateStatus(id, status);
            return ToResult(result);
        }
    }
}