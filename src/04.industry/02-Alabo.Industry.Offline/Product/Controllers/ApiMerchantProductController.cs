using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Offline.Merchants.Domain.Services;
using Alabo.App.Offline.Merchants.ViewModels;
using Alabo.App.Offline.Product.Domain.CallBacks;
using Alabo.App.Offline.Product.Domain.Entities;
using Alabo.App.Offline.Product.Domain.Services;
using Alabo.App.Offline.Product.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Maps;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Offline.Product.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/MerchantProduct/[action]")]
    public class ApiMerchantProductController : ApiBaseController<MerchantProduct, ObjectId>
    {
        public ApiMerchantProductController()
            : base()
        {
            BaseService = Resolve<IMerchantProductService>();
        }

        [HttpGet]
        [Display(Description = "������Ʒ")]
        public ApiResult<MerchantProductViewModel> GetMerchantProducts(string merchantStoreId)
        {

            //get merchant store
            //var merchant = Resolve<IMerchantStoreService>().GetMerchantByMerchantStoreId(merchantStoreId);
            //if (merchant == null || merchant.Item1 == null || merchant.Item2 == null)
            //{
            //    return ApiResult.Failure<MerchantProductViewModel>("��ǰ�̼�û�п�ͨ�ŵ�");
            //}

            //var merchantInfo = merchant.Item1;
            var merchantStore = Resolve<IMerchantStoreService>().FirstOrDefault();// merchant.Item2;//merchantStoreId
            //relation
            var allRelations = Resolve<IRelationService>().GetClass(typeof(MerchantProductClassRelation).FullName).ToList();//, merchantInfo.UserId
            //products filter merchant store id
            var allProducts = Resolve<IMerchantProductService>().GetList(); //p => p.MerchantStoreId == merchantStore.Id.ToString()
            var apiService = Resolve<IApiService>();
            var products = new List<MerchantProduct>();
            allRelations.ForEach(item =>
            {
                var tempProducts = allProducts.Where(p => p.ClassId == item.Id).ToList();
                tempProducts.ForEach(product =>
                {

                    product.ThumbnailUrl = apiService.ApiImageUrl(product.ThumbnailUrl);
                    product.Images = product.Images.Select(p => apiService.ApiImageUrl(p)).ToList();
                    product.Stock = product.Skus.Sum(s => s.Stock);
                    product.Description = string.Empty;
                    products.Add(product);
                });
            });

            var store = merchantStore.MapTo<MerchantStoreViewModel>();
            store.Logo = apiService.ApiImageUrl(store.Logo);
            var result = new MerchantProductViewModel
            {
                MerchantStore = store,
                Relations = allRelations.MapToList<RelationViewModel>(),
                Products = products
            };

            return ApiResult.Success(result);
        }



        [HttpGet]
        [Display(Description = "������Ʒ����")]
        public ApiResult<MerchantProduct> GetMerchantProduct(string merchantStoreProductId)
        {
            var merchantStore = Resolve<IMerchantStoreService>().FirstOrDefault();
            //relation
            var allRelations = Resolve<IRelationService>().GetClass(typeof(MerchantProductClassRelation).FullName).ToList();//, merchantInfo.UserId
            //products filter merchant store id
            var product = Resolve<IMerchantProductService>().GetSingle(s => s.Id == ObjectId.Parse(merchantStoreProductId)); //p => p.MerchantStoreId == merchantStore.Id.ToString()
            var apiService = Resolve<IApiService>();
            product.ThumbnailUrl = apiService.ApiImageUrl(product.ThumbnailUrl);
            product.Images = product.Images.Select(p => apiService.ApiImageUrl(p)).ToList();
            product.Stock = product.Skus.Sum(s => s.Stock);            
            return ApiResult.Success(product);
        }



        [HttpGet]
        [Display(Description = "������Ʒ")]
        public ApiResult<PagedList<MerchantProduct>> MerchantList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IMerchantProductService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// ɾ����Ʒ
        /// </summary>
        [HttpDelete]
        public ApiResult Delete(string Id)
        {
            if (Resolve<IMerchantProductService>().Delete(Id))
            {
                return ApiResult.Success("��Ʒɾ���ɹ�");
            }

            return ApiResult.Failure("��Ʒɾ��ʧ��,�����쳣�����Ժ�����");
        }


        #region ���ӵ�����¼

        /// <summary>
        ///     �޸Ļ����
        ///     ʹ��new ����base ��querySave����
        ///     �������ӵ�ʱ���Ƿ���,sku��һ��id��ֵ ��������idȫ��Ϊ0
        /// </summary>
        [HttpPost]
        [Display(Description = "�޸Ļ�ɾ��")]
        public new ApiResult<MerchantProduct> SaveMerchantProduct([FromBody]MerchantProduct paramaer)
        {
            if (BaseService == null)
            {
                return ApiResult.Failure<MerchantProduct>("���ڿ������ж���BaseService");
            }

            if (paramaer == null)
            {
                return ApiResult.Failure<MerchantProduct>("����ֵΪ��,��������������������ã�����ObjectId�Ƿ���������");
            }
            if (!this.IsFormValid())
            {
                return ApiResult.Failure<MerchantProduct>(this.FormInvalidReason());
            }

            foreach (var item in paramaer.Skus)
            {
                if (string.IsNullOrEmpty(item.SkuId) || item.SkuId == ObjectId.Empty.ToString() || item.SkuId == "000000000000000000000000")
                {
                    item.SkuId = ObjectId.GenerateNewId().ToString();
                }
            }

            var find = Resolve<IMerchantProductService>().GetSingle(paramaer.Id);
            if (find == null)
            {
                var result = Resolve<IMerchantProductService>().Add(paramaer);
                if (result == false)
                {
                    return ApiResult.Failure<MerchantProduct>("���ʧ��");
                }
                return ApiResult.Success(paramaer);
            }
            else
            {
                find = AutoMapping.SetValue<MerchantProduct>(paramaer);
                var result = Resolve<IMerchantProductService>().Update(find);
                if (result == false)
                {
                    return ApiResult.Failure<MerchantProduct>("���ʧ��");
                }

                return ApiResult.Success(find);
            }
        }

        #endregion ���ӵ�����¼



    }
}