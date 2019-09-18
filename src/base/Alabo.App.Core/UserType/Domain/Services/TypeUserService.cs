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

        #region ��ȡ����ļ�¼ ���ݻ�Ա��֯�ܹ�ͼ

        /// <summary>
        ///     ��ȡ����ļ�¼
        ///     ���ݻ�Ա��֯�ܹ�ͼ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="userTypeId">�û�����Id</param>
        public Entities.UserType GetNearestMap(long userId, Guid userTypeId) {
            var userMap = Resolve<IUserMapService>().GetSingle(r => r.UserId == userId);
            if (userMap != null) {
                var parentMaps = userMap.ParentMap.ToObject<List<ParentMap>>();
                var userParentUserIds = parentMaps.Select(r => r.UserId);
                // ���ҳ��ϼ������������û�����
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

        #endregion ��ȡ����ļ�¼ ���ݻ�Ա��֯�ܹ�ͼ

        #region ��ӵ����û� ��ȡ�����û�

        /// <summary>
        /// ��ӵ����û�
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        public void AddStoreUser(long storeId, long userId) {
            if (storeId <= 0 || userId <= 0) {
                throw new ValidException("����Id���û�Id����Ϊ��");
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
        /// ��ȡ�����û�
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public TypeUser GetStoreUser(long storeId, long userId) {
            var find = Resolve<ITypeUserService>().GetSingle(r =>
                r.UserTypeId == UserTypeEnum.Supplier.GetFieldId() && r.UserId == userId && r.TypeId == storeId);
            return find;
        }

        #endregion ��ӵ����û� ��ȡ�����û�
    }
}