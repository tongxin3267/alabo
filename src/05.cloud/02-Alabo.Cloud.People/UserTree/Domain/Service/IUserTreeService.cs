using Alabo.Cloud.People.UserTree.Domain.Configs;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using System.Collections.Generic;

namespace Alabo.Cloud.People.UserTree.Domain.Service
{
    /// <summary>
    ///     对用户表的组织结构进行操作
    /// </summary>
    public interface IUserTreeService : IService
    {
        /// <summary>
        ///     获取用户组织架构图
        /// </summary>
        /// <param name="userId">用户Id</param>
        List<Users.Entities.UserTree> GetUserTree(long userId);

        List<Users.Entities.UserTree> GetTree(long userId, UserTreeConfig userTreeConfig, UserTypeConfig userType,
            List<UserGradeConfig> userGradeConfigList, UserTypeConfig userServiceConfig);
    }
}