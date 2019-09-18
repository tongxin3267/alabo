using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Activitys.Domain.Repositories {

    public class ActivityRecordRepository : RepositoryEfCore<ActivityRecord, long>, IActivityRecordRepository {

        public ActivityRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}