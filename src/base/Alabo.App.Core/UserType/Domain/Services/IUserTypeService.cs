using System;
using System.Collections.Generic;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public interface IUserTypeService : IService<Entities.UserType, long> {
        /// <summary>
        ///     获取菜单类型
        /// </summary>
        /// <param name="typeEnum"></param>

        SideBarType GetSideBarType(UserTypeEnum typeEnum);

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
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeEnum">用户类Id</param>
        Entities.UserType GetSingle(long userId, UserTypeEnum userTypeEnum);

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

        /// <summary>
        ///     获取用户类型的详情
        /// </summary>
        /// <param name="id">主键ID</param>
        Entities.UserType GetSingleDetail(long id);

        /// <summary>
        ///     根据用户类型Id，和实体Id获取用户
        ///     比如根据省代理用户，获取区域代理用户等
        /// </summary>
        /// <param name="userTypeId">用户类型Id</param>
        /// <param name="eneityId">实体Id</param>
        User.Domain.Entities.User GetUserTypeUser(Guid userTypeId, long eneityId);

        /// <summary>
        ///     用户所有的等级，包括会员等级，用户类型等级
        /// </summary>
        /// <param name="userId">用户Id</param>
        IList<Guid> UserAllGradeId(long userId);

        IEnumerable<Type> GetAllTypes();

        /// <summary>
        ///     根据Guid获取用户类型的FullName
        /// </summary>
        /// <param name="userTypeId">用户类型Id</param>
        string GetUserTypeKey(Guid userTypeId);

        /// <summary>
        ///     根据用户类型等级获取用户类型的TypeId
        /// </summary>
        /// <param name="gradeKey"></param>
        Guid GetUserTypeIdByGradeKey(string gradeKey);

        /// <summary>
        ///     根据用户类型Id获取SideBar
        /// </summary>
        /// <param name="userTypeId">用户类型Id</param>
        SideBarType GetSideBarByKey(Guid userTypeId);

        void Update(Entities.UserType userType);

        /// <summary>
        ///     根据用户Id获取所有的类型
        /// </summary>
        /// <param name="userId">用户Id</param>
        IList<Entities.UserType> GetAllUserType(long userId);

        /// <summary>
        ///     检查用户是否包含所属的用户类型Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeIds"></param>
        bool HasUserType(long userId, List<string> userTypeIds);

        /// <summary>
        ///     检查用户是否包含所属的用户类型Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeIds"></param>
        bool HasUserType(long userId, List<Guid> userTypeIds);
    }
}