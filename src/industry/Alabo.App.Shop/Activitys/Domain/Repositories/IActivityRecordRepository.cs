using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Activitys.Domain.Repositories {

    /// <summary>
    ///     Interface IActivityRecoredRepository
    /// </summary>
    public interface IActivityRecordRepository : IRepository<ActivityRecord, long> {
    }
}