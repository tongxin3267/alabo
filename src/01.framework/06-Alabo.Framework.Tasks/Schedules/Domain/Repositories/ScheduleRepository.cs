using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Tasks.Schedules.Domain.Repositories
{
    public class ScheduleRepository : RepositoryMongo<Schedule, ObjectId>, IScheduleRepository
    {
        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}