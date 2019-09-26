using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Domains.Base.Repositories
{
    public class LogsRepository : RepositoryMongo<Logs, ObjectId>, ILogsRepository
    {
        public LogsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}