using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.Domains.Base.Services
{
    public class LogsService : ServiceBase<Logs, ObjectId>, ILogsService
    {
        public LogsService(IUnitOfWork unitOfWork, IRepository<Logs, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }
    }
}