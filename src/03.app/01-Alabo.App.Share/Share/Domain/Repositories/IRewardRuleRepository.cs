using MongoDB.Bson;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Share.Domain.Repositories {

    public interface IRewardRuleRepository : IRepository<RewardRule, ObjectId> {
    }
}