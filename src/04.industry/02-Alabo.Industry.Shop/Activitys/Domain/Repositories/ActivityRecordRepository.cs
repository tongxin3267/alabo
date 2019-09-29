using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Shop.Activitys.Domain.Entities;

namespace Alabo.Industry.Shop.Activitys.Domain.Repositories
{
    public class ActivityRecordRepository : RepositoryEfCore<ActivityRecord, long>, IActivityRecordRepository
    {
        public ActivityRecordRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}