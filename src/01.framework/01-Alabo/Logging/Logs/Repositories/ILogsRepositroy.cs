using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Logging.Logs.Repositories
{
    public interface ILogsRepository : IRepository<Entities.Logs, ObjectId>
    {
    }
}