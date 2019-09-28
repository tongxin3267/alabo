using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Dtos;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Entities;
using Alabo.Cloud.Shop.PresaleProducts.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Domain.Repositories;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Mapping;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.PresaleProducts.Domain.Services
{
    public class PresaleProductService : ServiceBase<PresaleProduct, ObjectId>, IPresaleProductService
    {
        private readonly Guid _presaleProductStyleId = new Guid("e0000000-1478-49bd-bfc7-e73a5d699111");
        private ObjectId _storeId;

        public PresaleProductService(IUnitOfWork unitOfWork, IRepository<PresaleProduct, ObjectId> repository)
            : base(unitOfWork, repository)
        {
        }

        /// <summary>
        ///     list
        /// </summary>
        /// <param name="productApiInput"></param>
        /// <returns></returns>
        public ProductItemApiOutput GetProducts(ProductApiInput productApiInput)
        {
            productApiInput.PriceStyleId = _presaleProductStyleId;
            var model = Repository<IProductRepository>().GetProductItems(productApiInput, out var count);
            //linked product
            var productIds = model.Select(p => p.Id).ToList();
            var linkedProducts = Repository<IPresaleProductRepository>()
                .GetList(p => productIds.Contains(p.ProductId) && p.PriceStyleId == _presaleProductStyleId)
                .ToList();
            model.ForEach(r =>
            {
                r.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(r.ThumbnailUrl);
                r.Price = decimal.Round(r.Price, 2);
                r.IsLinked = linkedProducts.Exists(p => p.ProductId == r.Id);
            });

            var apiOutput = new ProductItemApiOutput();
            apiOutput.ProductItems = model;
            apiOutput.TotalSize = count / productApiInput.PageSize + 1;
            return apiOutput;
        }

        /// <summary>
        ///     list
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PresaleProductItemApiOutput GetPresaleProducts(PresaleProductApiInput input)
        {
            input.PriceStyleId = _presaleProductStyleId;
            var model = Repository<IPresaleProductRepository>().GetPresaleProducts(input, out var count);
            model.ForEach(r =>
            {
                r.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(r.ThumbnailUrl);
                r.CostPrice = decimal.Round(r.CostPrice, 2);
            });
            var apiOutput = new PresaleProductItemApiOutput
            {
                ProductItems = model,
                TotalSize = count / input.PageSize + 1
            };
            return apiOutput;
        }

        /// <summary>
        ///     add
        /// </summary>
        /// <param name="presaleProducts"></param>
        /// <returns></returns>
        public ServiceResult AddPresaleProducts(IList<PresaleProductEdit> presaleProducts)
        {
            //check
            if (presaleProducts == null || presaleProducts.Count <= 0)
                return ServiceResult.FailedWithMessage("预售商品数据异常！");
            //Distinct
            var newPresaleProducts = presaleProducts
                .Where(p => p.ProductId > 0)
                .GroupBy(p => p.ProductId)
                .Select(p => p.First())
                .ToList();
            if (newPresaleProducts.Count <= 0) return ServiceResult.FailedWithMessage("预售商品数据异常！");

            //get exists ids
            var repository = Repository<IPresaleProductRepository>();
            var presaleProductIds = newPresaleProducts.Select(n => n.ProductId).ToList();
            var existsIds = repository
                .GetList(p => presaleProductIds.Contains(p.ProductId) && p.PriceStyleId == _presaleProductStyleId)
                .Select(p => p.ProductId)
                .ToList();
            //add
            //remove ids of exists
            var datas = new List<PresaleProduct>();
            newPresaleProducts
                .Where(p => !existsIds.Contains(p.ProductId))
                .ToList()
                .ForEach(item =>
                {
                    var tempItem = AutoMapping.SetValue<PresaleProduct>(item);
                    // tempItem.Id = o;
                    tempItem.StoreId = _storeId;
                    tempItem.PriceStyleId = _presaleProductStyleId;
                    tempItem.Status = (int)ProductStatus.Online;
                    datas.Add(tempItem);
                });
            if (datas.Count > 0) repository.AddMany(datas);

            return ServiceResult.Success;
        }

        /// <summary>
        ///     update
        /// </summary>
        /// <param name="presaleProduct"></param>
        /// <returns></returns>
        public ServiceResult UpdatePresaleProduct(PresaleProductEdit presaleProduct)
        {
            if (presaleProduct == null || presaleProduct.Id <= 0) return ServiceResult.FailedWithMessage("预售商品数据异常！");
            //update
            var repository = Repository<IPresaleProductRepository>();
            var data = repository.GetSingle(presaleProduct.Id);
            data.VirtualPrice = presaleProduct.VirtualPrice;
            data.Sort = presaleProduct.Sort;
            repository.UpdateSingle(data);

            return ServiceResult.Success;
        }

        /// <summary>
        ///     update status
        /// </summary>
        /// <returns></returns>
        public ServiceResult UpdateStatus(long id, ProductStatus status)
        {
            //update
            var repository = Repository<IPresaleProductRepository>();
            var data = repository.GetSingle(id);
            if (data == null) ServiceResult.FailedWithMessage("产品不存在");
            data.Status = (int)status;
            repository.UpdateSingle(data);

            return ServiceResult.Success;
        }
    }
}