using System.Collections.Generic;
using Alabo.App.Share.Share.Domain.Dto;
using Alabo.Domains.Repositories;
using RewardModel = Alabo.App.Share.Share.Domain.Entities.Reward;

namespace Alabo.App.Share.Share.Domain.Repositories {

    public interface IRewardRepository : IRepository<RewardModel, long> {

        IList<RewardModel> GetRewardList(RewardInput userInput, out long count);
    }
}