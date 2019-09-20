using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Share.Kpi.Domain.CallBack;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.App.Share.Kpi.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Kpi.Domain.Services {

    public interface IGradeKpiService : IService<GradeKpi, ObjectId> {
        /// <summary>
        /// ��Ч����ͳ��
        /// </summary>

        void TaskQueue();

        PagedList<GradeKpi> GetGradeKpiList(object query);

        /// <summary>
        /// �������л�Ա��Kpi
        /// ��������Kpi�ͽ���Kpi
        /// </summary>
        void KpiOperatorAllUser();

        /// <summary>
        /// �����û����㣬����Kpi
        /// </summary>
        /// <param name="user">�û�</param>
        GradeKpi PromotedKpiOperator(User user);

        /// <summary>
        /// �����û����㣬��ְKpi
        /// </summary>
        /// <param name="user">�û�</param>
        GradeKpi DemotionKpiOperator(User user);

        /// <summary>
        /// �����û�����KpiItem���������Kpi���
        /// </summary>
        /// <param name="user">�û�</param>
        /// <param name="kpiItems">Kpi��Ŀ</param>
        /// <param name="logicalOperator">�߼�Ԥ���</param>

        Tuple<bool, List<GradeKpiItem>> KpiItemOperator(User user, IList<KpiItem> kpiItems, string logicalOperator, TimeType timeType);

        /// <summary>
        /// ��ȡ�ȼ��޸���ͼ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GradeChangeView GetGradeChangeView(object id);

        /// <summary>
        /// ����ȼ��޸�
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult SaveChangeGrade(GradeChangeView view);
    }
}