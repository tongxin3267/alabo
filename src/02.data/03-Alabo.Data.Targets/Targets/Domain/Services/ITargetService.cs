using Alabo.Data.Targets.Targets.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Targets.Targets.Domain.Services
{
    public interface ITargetService : IService<Target, ObjectId>
    {
    }
}