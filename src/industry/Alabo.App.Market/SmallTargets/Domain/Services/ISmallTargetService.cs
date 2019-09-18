using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Market.SmallTargets.Domain.Services {

    public interface ISmallTargetService : IService<SmallTarget, ObjectId> {
    }
}