using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SmallTargets.Domain.Repositories {

    public interface IUserSmallTargetRepository : IRepository<UserSmallTarget, ObjectId> {
    }
}