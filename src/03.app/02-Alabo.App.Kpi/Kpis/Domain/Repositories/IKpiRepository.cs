using Alabo.App.Kpis.Kpis.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Kpis.Kpis.Domain.Repositories
{
    /// <summary>
    ///     Interface IKPIRepository
    /// </summary>
    public interface IKpiRepository : IRepository<Kpi, long>
    {
        /// <summary>
        ///     根据用户Id，统计类型Id,获取最新的快照
        /// </summary>
        /// <param name="kpi"></param>
        /// <returns>KPI.</returns>
        Kpi GetLastSingle(Kpi kpi);
    }
}