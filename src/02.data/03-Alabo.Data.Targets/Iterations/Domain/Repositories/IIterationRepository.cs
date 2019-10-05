using Alabo.Data.Targets.Iterations.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Iterations.Domain.Repositories
{
    public interface IIterationRepository : IRepository<Iteration, ObjectId>
    {
    }
}