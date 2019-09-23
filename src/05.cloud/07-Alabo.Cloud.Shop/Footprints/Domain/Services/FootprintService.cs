using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Share.Attach.Domain.Dtos;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Share.Attach.Domain.Services {

    public class FootprintService : ServiceBase<Footprint, ObjectId>, IFootprintService {

        public FootprintService(IUnitOfWork unitOfWork, IRepository<Footprint, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public new ServiceResult Add(FootprintInput footprintInput) {
            var product = Resolve<IProductService>().GetSingle(u => u.Id == footprintInput.EntityId.ToInt64());
            var productDetail = Resolve<IProductDetailService>().GetSingle(u => u.ProductId == footprintInput.EntityId.ToInt64());
            var image = productDetail.ImageJson.DeserializeJson<List<ProductThum>>();
            Footprint footprint = new Footprint {
                EntityId = footprintInput.EntityId,
                Name = product.Name,
                Url = $"/product/show?id={product.Id}",
                Image = image[0].ThumbnailUrl,
                UserId = footprintInput.LoginUserId,
                Type = footprintInput.Type
            };
            var temp = Resolve<IFootprintService>().GetSingle(u =>
              u.EntityId == footprintInput.EntityId && u.UserId == footprintInput.LoginUserId);
            if (temp == null) {
                var result = Add(footprint);
                return ServiceResult.Success;
            }

            return ServiceResult.Failed;
        }

        public ServiceResult Clear(long loginUserId) {
            throw new System.NotImplementedException();
        }

        public PagedList<Footprint> GetProductPagedList(object query) {
            return GetPagedList(query);
        }
    }
}