using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Activitys.Domain.Entities;

namespace Alabo.Industry.Shop.Activitys.Domain.Services {

    /// <summary>
    ///     Class ActivityService.
    /// </summary>
    public class ActivityService : ServiceBase<Activity, long>, IActivityService {

        public ActivityService(IUnitOfWork unitOfWork, IRepository<Activity, long> repository) : base(unitOfWork, repository) {
        }
    }
}