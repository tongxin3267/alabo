using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Shop.Activitys.Domain.Repositories {

    public class ActivityRepository : RepositoryEfCore<Activity, long>, IActivityRepository {

        public ActivityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}