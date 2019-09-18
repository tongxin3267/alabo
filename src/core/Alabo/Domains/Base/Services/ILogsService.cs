using MongoDB.Bson;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Services;

namespace Alabo.Domains.Base.Services
{
    public interface ILogsService : IService<Logs, ObjectId>
    {
    }
}