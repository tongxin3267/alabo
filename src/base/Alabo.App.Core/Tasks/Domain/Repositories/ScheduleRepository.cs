using MongoDB.Bson;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Tasks.Domain.Repositories {

    public class ScheduleRepository : RepositoryMongo<Schedule, ObjectId>, IScheduleRepository {

        public ScheduleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}