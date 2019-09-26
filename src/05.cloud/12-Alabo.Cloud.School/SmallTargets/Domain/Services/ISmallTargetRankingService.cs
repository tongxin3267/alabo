using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Services {

    public interface ISmallTargetRankingService : IService<SmallTargetRanking, ObjectId> {
    }
}