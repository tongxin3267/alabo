using System;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public interface ITypeUserService : IService<TypeUser, long> {

        /// <summary>
        ///     获取最近的记录
        ///     根据会员组织架构图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeId">用户类型Id</param>
        Entities.UserType GetNearestMap(long userId, Guid userTypeId);

        void SetTypeUser(long userId, Guid userTypeId);

        /// <summary>
        /// 添加店铺用户
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        void AddStoreUser(long storeId, long userId);

        /// <summary>
        /// 获取店铺用户
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        TypeUser GetStoreUser(long storeId, long userId);
    }
}