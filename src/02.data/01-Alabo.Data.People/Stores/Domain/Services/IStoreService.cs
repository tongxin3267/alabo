using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Deliveries.ViewModels;
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
        /// <param name="UserId">��ԱId</param>
        Store GetUserStore(long UserId);
    }
}