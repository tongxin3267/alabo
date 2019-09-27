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
        ///     获取自营店铺
        ///     平台店铺，后台添加的时候，为平台商品
        /// </summary>
        Store PlatformStore();

        /// <summary>
        ///     获取s the 会员 store.
        ///     获取会员店铺
        /// </summary>
        /// <param name="UserId">会员Id</param>
        Store GetUserStore(long UserId);
    }
}