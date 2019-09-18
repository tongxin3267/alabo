using MongoDB.Bson;
using Alabo.App.Open.Share.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Share.Domain.Repositories {

    public interface IRewardRuleRepository : IRepository<RewardRule, ObjectId> {
    }
}