using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Logging.Logs.Services
{
    public interface ILogsService : IService<Entities.Logs, ObjectId>
    {
    }
}