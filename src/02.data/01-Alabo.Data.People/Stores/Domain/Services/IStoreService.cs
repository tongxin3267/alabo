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
        Store PlanformStore();

        /// <summary>
        ///     获取s the 会员 store.
        ///     获取会员店铺
        /// </summary>
        /// <param name="UserId">会员Id</param>
        Store GetUserStore(long UserId);

        /// <summary>
        ///     添加s the 或 更新.
        /// </summary>
        /// <param name="store">The store.</param>
        ServiceResult AddOrUpdate(ViewStore store);

        /// <summary>
        ///     获取s the 视图 store 分页 list.
        /// </summary>
        /// <param name="dto">The dto.</param>
        PagedList<ViewStore> GetViewStorePageList(PagedInputDto dto);

        /// <summary>
        ///     获取s the 视图 store.
        /// </summary>
        /// <param name="Id">Id标识</param>
        ViewStore GetViewStore(long Id);

        /// <summary>
        ///     获取s the 分页 list.
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<ViewStore> GetPageList(object query);

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ViewStore GetView(long id);
    }
}