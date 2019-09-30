using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Cloud.Shop.Footprints.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.ViewModels;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Alabo.Cloud.Shop.Footprints.Domain.Services
{
    public class FootprintService : ServiceBase<Footprint, ObjectId>, IFootprintService
    {
        public FootprintService(IUnitOfWork unitOfWork, IRepository<Footprint, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        public ServiceResult Add(FootprintInput footprintInput)
        {
            var product = Resolve<IProductService>().GetSingle(u => u.Id == footprintInput.EntityId.ToInt64());
            var productDetail = Resolve<IProductDetailService>()
                .GetSingle(u => u.ProductId == footprintInput.EntityId.ToInt64());
            var image = productDetail.ImageJson.DeserializeJson<List<ProductThum>>();
            var footprint = new Footprint
            {
                EntityId = footprintInput.EntityId,
                Name = product.Name,
                Url = $"/product/show?id={product.Id}",
                Image = image[0].ThumbnailUrl,
                UserId = footprintInput.LoginUserId,
                Type = footprintInput.Type
            };
            var temp = Resolve<IFootprintService>().GetSingle(u =>
                u.EntityId == footprintInput.EntityId && u.UserId == footprintInput.LoginUserId);
            if (temp == null)
            {
                var result = Add(footprint);
                return ServiceResult.Success;
            }

            return ServiceResult.Failed;
        }

        public ServiceResult Clear(long loginUserId)
        {
            throw new NotImplementedException();
        }

        public PagedList<Footprint> GetProductPagedList(object query)
        {
            return GetPagedList(query);
        }
    }
}