using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Shop.Activitys.Domain.Services {

    /// <summary>
    ///     Class ActivityService.
    /// </summary>
    public class ActivityService : ServiceBase<Activity, long>, IActivityService {

        public ActivityService(IUnitOfWork unitOfWork, IRepository<Activity, long> repository) : base(unitOfWork, repository) {
        }
    }
}