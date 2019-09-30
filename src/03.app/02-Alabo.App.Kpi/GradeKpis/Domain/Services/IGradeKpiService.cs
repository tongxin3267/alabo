using Alabo.App.Kpis.GradeKpis.Domain.Entities;
using Alabo.App.Kpis.GradeKpis.Dtos;
using Alabo.App.Kpis.Kpis.Domain.Configs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Users.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Alabo.App.Kpis.GradeKpis.Domain.Services
{
    public interface IGradeKpiService : IService<GradeKpi, ObjectId>
    {
        /// <summary>
        ///     绩效数据统计
        /// </summary>
        void TaskQueue();

        PagedList<GradeKpi> GetGradeKpiList(object query);

        /// <summary>
        ///     计算所有会员的Kpi
        ///     包括晋升Kpi和降级Kpi
        /// </summary>
        void KpiOperatorAllUser();

        /// <summary>
        ///     根据用户计算，晋升Kpi
        /// </summary>
        /// <param name="user">用户</param>
        GradeKpi PromotedKpiOperator(User user);

        /// <summary>
        ///     根据用户计算，降职Kpi
        /// </summary>
        /// <param name="user">用户</param>
        GradeKpi DemotionKpiOperator(User user);

        /// <summary>
        ///     根据用户、和KpiItem配置项，计算Kpi结果
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="kpiItems">Kpi项目</param>
        /// <param name="logicalOperator">逻辑预算符</param>
        Tuple<bool, List<GradeKpiItem>> KpiItemOperator(User user, IList<KpiItem> kpiItems, string logicalOperator,
            TimeType timeType);

        /// <summary>
        ///     获取等级修改视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GradeChangeView GetGradeChangeView(object id);

        /// <summary>
        ///     保存等级修改
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        ServiceResult SaveChangeGrade(GradeChangeView view);
    }
}