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

    public class FavoriteService : ServiceBase<Favorite, ObjectId>, IFavoriteService {

        public FavoriteService(IUnitOfWork unitOfWork, IRepository<Favorite, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public new ServiceResult Add(FavoriteInput favoriteInput) {
            //if (favoriteInput.Type == FavoriteType.Product) {
            var product = Resolve<IProductService>().GetSingle(u => u.Id == favoriteInput.EntityId.ToInt64());
            var productDetail = Resolve<IProductDetailService>().GetSingle(u => u.ProductId == favoriteInput.EntityId.ToInt64());
            var image = productDetail.ImageJson.DeserializeJson<List<ProductThum>>();
            Favorite favorite = new Favorite {
                EntityId = favoriteInput.EntityId,
                Type = favoriteInput.Type,
                UserId = favoriteInput.LoginUserId,
                Name = product.Name,
                Image = image[0].ThumbnailUrl
            };
            var favoriteSingle = Resolve<IFavoriteService>().GetSingle(u => u.EntityId == favoriteInput.EntityId && u.UserId == favoriteInput.LoginUserId);

            if (favoriteSingle == null) {
                var result = Add(favorite);
                if (result) {
                    return ServiceResult.Success;
                }
            }
            return ServiceResult.Failed;
            // }
        }

        public PagedList<Favorite> GetProductPagedList(object query) {
            return GetPagedList(query);
        }

        /// <summary>
        /// 根据用户id获取收藏数量
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public long GetFavoriteCountByUserId(long userId) {
            var res = Resolve<IFavoriteService>().GetList(s => s.UserId == userId);

            return res.Count;
        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="favoriteInput"></param>
        /// <returns></returns>
        public ServiceResult Remove(FavoriteInput favoriteInput) {
            var favoriteSingle = Resolve<IFavoriteService>().GetSingle(u => u.EntityId == favoriteInput.EntityId && u.UserId == favoriteInput.LoginUserId);
            if (favoriteSingle != null) {
                Delete(favoriteSingle);
                return ServiceResult.Success;
            }
            return ServiceResult.Failed;
        }
    }
}