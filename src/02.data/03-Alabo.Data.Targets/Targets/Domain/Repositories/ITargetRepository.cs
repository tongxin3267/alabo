using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Targets.Domain.Repositories
{
    public interface ITargetRepository : IRepository<Target, ObjectId>
    {
    }
}