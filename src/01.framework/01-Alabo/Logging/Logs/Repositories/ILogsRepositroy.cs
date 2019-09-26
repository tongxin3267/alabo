using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Domains.Base.Repositories
{
    public interface ILogsRepository : IRepository<Logs, ObjectId>
    {
    }
}