using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Repositories
{
    public interface IUserSmallTargetRepository : IRepository<UserSmallTarget, ObjectId>
    {
    }
}