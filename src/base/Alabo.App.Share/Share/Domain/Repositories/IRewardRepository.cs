using System.Collections.Generic;
using Alabo.App.Open.Share.Domain.Dto;
using Alabo.Domains.Repositories;
using RewardModel = Alabo.App.Open.Share.Domain.Entities.Reward;

namespace Alabo.App.Open.Share.Domain.Repositories {

    public interface IRewardRepository : IRepository<RewardModel, long> {

        IList<RewardModel> GetRewardList(RewardInput userInput, out long count);
    }
}