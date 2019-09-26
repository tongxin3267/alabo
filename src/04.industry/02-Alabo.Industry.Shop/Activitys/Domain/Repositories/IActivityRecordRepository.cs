using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Activitys.Domain.Entities;

namespace Alabo.Industry.Shop.Activitys.Domain.Repositories
{
    /// <summary>
    ///     Interface IActivityRecoredRepository
    /// </summary>
    public interface IActivityRecordRepository : IRepository<ActivityRecord, long>
    {
    }
}