using Alabo.Domains.Repositories;
using Alabo.Users.Entities;

namespace Alabo.Data.People.Users.Domain.Repositories
{
    public interface IUserMapRepository : IRepository<UserMap, long>
    {
        /// <summary>
        ///     获取组织架构图信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        UserMap GetParentMap(long userId);

        /// <summary>
        ///     获取完整的组织架构图信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        UserMap GetSingle(long userId);

        UserMap Add(UserMap userMap);

        void UpdateMap(long userId, string map);

        /// <summary>
        ///     根据下面的会员，更新团队信息
        /// </summary>
        /// <param name="childuUserId"></param>
        void UpdateTeamInfo(long childuUserId = 0);
    }
}