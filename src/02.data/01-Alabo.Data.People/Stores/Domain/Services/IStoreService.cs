using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Services
{
    public interface IStoreService : IService<Store, ObjectId>
    {
        /// <summary>
        ///     ��ȡ��Ӫ����
        ///     ƽ̨���̣���̨��ӵ�ʱ��Ϊƽ̨��Ʒ
        /// </summary>
        Store PlatformStore();

        /// <summary>
        ///     ��ȡs the ��Ա store.
        ///     ��ȡ��Ա����
        /// </summary>
        /// <param name="userId">��ԱId</param>
        Store GetUserStore(long userId);
    }
}