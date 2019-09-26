using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users.Domain.Services {

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
        /// 会员物理删除后，修改推荐人
        /// </summary>
        /// <returns></returns>
        ServiceResult UpdateParentUserAfterUserDelete(long userId, long parentId);
    }
}