using Alabo.App.Share.RewardRuless.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.App.Share.RewardRuless.Domain.Repositories
{
    public class RewardRuleRepository : RepositoryMongo<RewardRule, ObjectId>, IRewardRuleRepository
    {
        public RewardRuleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}