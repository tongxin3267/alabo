using Alabo.Domains.Repositories;
using Alabo.Framework.Tasks.Schedules.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Tasks.Schedules.Domain.Repositories
{
    public interface IScheduleRepository : IRepository<Schedule, ObjectId>
    {
    }
}