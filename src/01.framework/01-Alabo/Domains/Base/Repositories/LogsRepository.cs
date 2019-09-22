using MongoDB.Bson;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.Domains.Base.Repositories
{
    public class LogsRepository : RepositoryMongo<Logs, ObjectId>, ILogsRepository
    {
        public LogsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}