using MongoDB.Bson;
using Alabo.App.Share.Attach.Domain.Dtos;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Attach.Domain.Services {

    /// <summary>
    /// �ղؽӿ�
    /// </summary>
    public interface IFavoriteService : IService<Favorite, ObjectId> {

        /// <summary>
        /// ����ղ�
        /// </summary>
        /// <param name="favoriteInput"></param>
        /// <returns></returns>
        ServiceResult Add(FavoriteInput favoriteInput);

        /// <summary>
        /// �Ƴ� �ղ�
        /// </summary>
        /// <param name="favoriteInput"></param>
        /// <returns></returns>
        ServiceResult Remove(FavoriteInput favoriteInput);

        /// <summary>
        /// ͨ���û�id��ȡ�ղ�����
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        long GetFavoriteCountByUserId(long userId);
    }
}