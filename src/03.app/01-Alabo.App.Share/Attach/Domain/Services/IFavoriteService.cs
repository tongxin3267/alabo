using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Dtos;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Attach.Domain.Services {

    /// <summary>
    /// 收藏接口
    /// </summary>
    public interface IFavoriteService : IService<Favorite, ObjectId> {

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="favoriteInput"></param>
        /// <returns></returns>
        ServiceResult Add(FavoriteInput favoriteInput);

        /// <summary>
        /// 移除 收藏
        /// </summary>
        /// <param name="favoriteInput"></param>
        /// <returns></returns>
        ServiceResult Remove(FavoriteInput favoriteInput);

        /// <summary>
        /// 通过用户id获取收藏总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        long GetFavoriteCountByUserId(long userId);
    }
}