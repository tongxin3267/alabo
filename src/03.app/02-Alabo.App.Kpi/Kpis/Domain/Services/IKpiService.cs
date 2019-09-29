using System.Collections.Generic;
using Alabo.App.Kpis.Kpis.Domain.Entities;
using Alabo.App.Kpis.Kpis.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Users.Entities;

namespace Alabo.App.Kpis.Kpis.Domain.Services
{
    public interface IKpiService : IService<Kpi, long>
    {
        /// <summary>
        ///     添加单条统计记录
        ///     增加快照记录，同时记录新的值
        /// </summary>
        /// <param name="kpi">The KPI.</param>
        void AddSingle(Kpi kpi);

        /// <summary>
        ///     获取最后一条记录
        /// </summary>
        /// <param name="kpi"></param>
        Kpi GetLastSingle(Kpi kpi);

        /// <summary>
        ///     Gets the kpi list.
        /// </summary>
        /// <param name="query">查询</param>
        PagedList<KpiView> GetKpiList(object query);

        /// <summary>
        ///     根据Kpi访问和用户获取团队方式
        /// </summary>
        /// <param name="kpiTeamType"></param>
        /// <param name="user">用户</param>
        IList<User> GetUserByKpiTeamType(KpiTeamType kpiTeamType, User user);
    }
}