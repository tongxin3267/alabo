using Alabo.Data.Targets.Iterations.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Iterations.Domain.Services
{
    public interface IIterationService : IService<Iteration, ObjectId>
    {
    }
}