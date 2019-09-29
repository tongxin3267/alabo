using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Logging.Logs.Repositories
{
    public class LogsRepository : RepositoryMongo<Entities.Logs, ObjectId>, ILogsRepository
    {
        public LogsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}