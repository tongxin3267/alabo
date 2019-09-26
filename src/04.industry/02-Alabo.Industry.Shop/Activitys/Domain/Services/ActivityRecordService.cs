using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Activitys.Domain.Entities;

namespace Alabo.Industry.Shop.Activitys.Domain.Services {

    /// <summary>
    /// </summary>
    public class ActivityRecordService : ServiceBase<ActivityRecord, long>, IActivityRecordService {

        public ActivityRecordService(IUnitOfWork unitOfWork, IRepository<ActivityRecord, long> repository) : base(unitOfWork, repository) {
        }
    }
}