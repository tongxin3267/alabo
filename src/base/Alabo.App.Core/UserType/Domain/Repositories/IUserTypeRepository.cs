using System;
using System.Collections.Generic;
using Alabo.App.Core.UserType.Domain.Dtos;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.UserType.Domain.Repositories {

    public interface IUserTypeRepository : IRepository<Entities.UserType, long> {

        /// <summary>
        ///     从缓存中获取UserType
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeId">用户类Id</param>
        Entities.UserType GetSingle(long userId, Guid userTypeId);

        /// <summary>
        ///     从缓存中获取UserType
        ///     比如获取省代理，城市代理，区域代理,门店，供应商等
        /// </summary>
        /// <param name="userTypeId">用户类Id</param>
        /// <param name="entityId">用户Id</param>
        Entities.UserType GetSingle(Guid userTypeId, long entityId);

        /// <summary>
        ///     根据Id获取,从缓存中获取UserType
        /// </summary>
        /// <param name="id">主键ID</param>
        Entities.UserType GetSingle(long id);

        IList<Entities.UserType> GetViewUserTypeList(UserTypeInput userTypeInput, out long count);

        IList<Guid> UserAllGradeId(long userId);
    }
}