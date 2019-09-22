using MongoDB.Bson;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Share.Share.Domain.Repositories {

    public class RewardRuleRepository : RepositoryMongo<RewardRule, ObjectId>, IRewardRuleRepository {

        public RewardRuleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}