using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Services
{
    public interface IStoreService : IService<Store, ObjectId>
    {
        /// <summary>
        ///     获取自营店铺
        ///     平台店铺，后台添加的时候，为平台商品
        /// </summary>
        Store PlatformStore();

        /// <summary>
        ///     获取s the 会员 store.
        ///     获取会员店铺
        /// </summary>
        /// <param name="userId">会员Id</param>
        Store GetUserStore(long userId);
    }
}