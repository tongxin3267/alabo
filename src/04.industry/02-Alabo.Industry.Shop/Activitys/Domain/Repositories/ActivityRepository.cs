using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Activitys.Domain.Entities;

namespace Alabo.Industry.Shop.Activitys.Domain.Repositories {

    public class ActivityRepository : RepositoryEfCore<Activity, long>, IActivityRepository {

        public ActivityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}