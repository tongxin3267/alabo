using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Logging.Logs.Services
{
    public class LogsService : ServiceBase<Entities.Logs, ObjectId>, ILogsService
    {
        public LogsService(IUnitOfWork unitOfWork, IRepository<Entities.Logs, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}