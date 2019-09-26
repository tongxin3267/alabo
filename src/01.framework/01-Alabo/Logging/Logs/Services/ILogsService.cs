using Alabo.Domains.Base.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Domains.Base.Services
{
    public interface ILogsService : IService<Logs, ObjectId>
    {
    }
}