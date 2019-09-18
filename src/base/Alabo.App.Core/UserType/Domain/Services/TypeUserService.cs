using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;

namespace Alabo.App.Core.UserType.Domain.Services {

    public class TypeUserService : ServiceBase<TypeUser, long>, ITypeUserService {

        public TypeUserService(IUnitOfWork unitOfWork, IRepository<TypeUser, long> repository) : base(unitOfWork, repository) {
        }

        #region 获取最近的记录 根据会员组织架构图

        /// <summary>
        ///     获取最近的记录
        ///     根据会员组织架构图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userTypeId">用户类型Id</param>
        public Entities.UserType GetNearestMap(long userId, Guid userTypeId) {
            var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
            if (userMap != null) {
                var parentMaps = userMap.ParentMap.ToObject<List<ParentMap>>();
                var userParentUserIds = parentMaps.Select(r => r.UserId);
                // 查找出上级符合条件的用户类型
                var userTypes = Resolve<IUserTypeService>()
                    .GetList(r => r.UserTypeId == userTypeId && userParentUserIds.Contains(r.UserId))
                    .OrderBy(r => r.UserId);
                var nearestUserType = userTypes.FirstOrDefault();
                return nearestUserType;
            }

            return null;
        }

        public void SetTypeUser(long userId, Guid userTypeId) {
            var userType = GetNearestMap(userId, userTypeId);
            if (userType != null) {
                //var userTypeMap = new UserTypeMap {
                //    TypeUserId = userType.UserId,
                //    UserId = userId,
                //    TypeId = userType.Id,
                //    UserTypeId = userTypeId
                //};
                //Add(userTypeMap);
            }
        }

        #endregion 获取最近的记录 根据会员组织架构图

        #region 添加店铺用户 获取店铺用户

        /// <summary>
        /// 添加店铺用户
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        public void AddStoreUser(long storeId, long userId) {
            if (storeId <= 0 || userId <= 0) {
                throw new ValidException("店铺Id或用户Id不能为空");
            }
            var find = GetStoreUser(storeId, userId);
            if (find == null) {
                find = new TypeUser {
                    UserTypeId = UserTypeEnum.Supplier.GetFieldId(),
                    UserId = userId,
                    TypeId = storeId,
                };
                Resolve<ITypeUserService>().Add(find);
            }
        }

        /// <summary>
        /// 获取店铺用户
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TypeUser GetStoreUser(long storeId, long userId) {
            var find = Resolve<ITypeUserService>().GetSingle(r =>
                r.UserTypeId == UserTypeEnum.Supplier.GetFieldId() && r.UserId == userId && r.TypeId == storeId);
            return find;
        }

        #endregion 添加店铺用户 获取店铺用户
    }
}