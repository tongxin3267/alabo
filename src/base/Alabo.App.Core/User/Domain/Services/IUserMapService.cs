using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Entities.Extensions;
using Alabo.App.Core.User.ViewModels;
using Alabo.App.Core.User.ViewModels.Admin;
using Alabo.Domains.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public interface IUserMapService : IService<UserMap, long> {

        /// <summary>
        ///     获取组织架构图信息,优先从缓存中读取
        /// </summary>
        /// <param name="userId">用户Id</param>
        UserMap GetParentMapFromCache(long userId);

        /// <summary>
        ///     获取完整的组织架构图信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        UserMap GetSingle(long userId);

        /// <summary>
        ///     根据ParentId计算组织架构图
        /// </summary>
        /// <param name="ParentId"></param>
        string GetParentMap(long ParentId);

        void UpdateMap(long userId, long parentId);

        /// <summary>
        ///     根据推荐人Id获取用户列表
        /// </summary>
        /// <param name="parendId"></param>
        List<Entities.User> GetList(long parendId);

        List<long> GetChildUserIds(long parentId);

        /// <summary>
        /// 根据下面的会员，更新团队信息
        /// </summary>
        /// <param name="childuUserId"></param>
        void UpdateTeamInfo(long childuUserId = 0);

        /// <summary>
        ///     获取团队用户
        ///     根据UserMap.childNode字段获取
        /// </summary>
        /// <param name="userMap"></param>
        IEnumerable<Entities.User> GetTeamUser(UserMap userMap);

        /// <summary>
        ///     更新销售业绩
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="shopSaleExtension"></param>
        [Method(RunInUrl = true)]
        void UpdateShopSale(long userId, ShopSaleExtension shopSaleExtension);

        /// <summary>
        ///     更新所有用户的组织架构图
        /// </summary>
        void UpdateAllUserParentMap();

        /// <summary>
        /// 更新组织架构图队列
        /// </summary>
        void ParentMapTaskQueue();

        /// <summary>
        ///     获取直推会员、间推、团队的等级分布
        /// </summary>
        /// <param name="query"></param>
        PagedList<UserGradeInfoView> GetUserGradeInfoPageList(object query);

        /// <summary>
        /// 获取推荐人更新视图
        /// </summary>
        /// <returns></returns>
        ViewRelationUpdate GetUpdateParentUserView(object id);

        /// <summary>
        /// 获取转移团队关系视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ViewTransferRelationship GetTransferRelationship(object id);

        /// <summary>
        /// 转移团队关系
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult TransferRelationship(ViewTransferRelationship view);

        /// <summary>
        /// 更新推荐人
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult UpdateParentUser(ViewRelationUpdate view);

        /// <summary>
        /// 会员物理删除后，修改推荐人
        /// </summary>
        /// <returns></returns>
        ServiceResult UpdateParentUserAfterUserDelete(long userId, long parentId);
    }
}