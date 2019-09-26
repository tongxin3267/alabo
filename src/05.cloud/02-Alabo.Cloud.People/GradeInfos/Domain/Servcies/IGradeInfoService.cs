using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Attributes;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public interface IGradeInfoService : IService<GradeInfo, ObjectId> {

        /// <summary>
        /// ��ȡ��ǰ��Ա�ȼ��Ƿ���������
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Tuple<bool, Guid> GetKpiResult(GradeInfo gradeInfo, Users.Entities.User user);

        /// <summary>
        /// ��С��Ŀ��Kpi����
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="autoUpgradeItem"></param>
        /// <returns></returns>
        bool GetKpiItemResult(GradeInfo gradeInfo, AutoUpgradeItem autoUpgradeItem);

        /// <summary>
        /// ��Ӵ������л��еĺ�̨����
        /// </summary>
        void UpdataAllUserBackJob();

        /// <summary>
        ///     ���µ����û��ĵȼ���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        [Method(RunInUrl = true)]
        void UpdateSingle(long userId);

        /// <summary>
        ///     ���������õ��Ŷӵȼ�
        /// </summary>
        void UpdateAllUser();

        /// <summary>
        /// �����û����¸��û����Ŷӵȼ�
        /// �����Ա�Զ�����Kpi��
        /// ����������������Ա�Զ�������ͬʱ�����ϼ��Ŷ���Ϣ����ӻ�Ա�ȼ���¼
        /// </summary>
        /// <param name="userId"></param>
        void TeamUserGradeAutoUpdate(long userId);

        /// <summary>
        ///     �����û���ȡ�û�������ͳ��
        /// </summary>
        /// <param name="users"></param>
        IEnumerable<GradeInfoItem> GetUsersGradeInfo(IEnumerable<Users.Entities.User> users);
    }
}