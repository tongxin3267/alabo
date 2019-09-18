using System;
using Alabo.App.Core.UserType.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    public interface ITypeUserService : IService<TypeUser, long> {

        /// <summary>
        ///     ��ȡ����ļ�¼
        ///     ���ݻ�Ա��֯�ܹ�ͼ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        /// <param name="userTypeId">�û�����Id</param>
        Entities.UserType GetNearestMap(long userId, Guid userTypeId);

        void SetTypeUser(long userId, Guid userTypeId);

        /// <summary>
        /// ��ӵ����û�
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        void AddStoreUser(long storeId, long userId);

        /// <summary>
        /// ��ȡ�����û�
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        TypeUser GetStoreUser(long storeId, long userId);
    }
}