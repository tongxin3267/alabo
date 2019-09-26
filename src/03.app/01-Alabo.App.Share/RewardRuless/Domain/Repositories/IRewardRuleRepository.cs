using Alabo.App.Share.RewardRuless.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Share.RewardRuless.Domain.Repositories
{
    public interface IRewardRuleRepository : IRepository<RewardRule, ObjectId>
    {
    }
}