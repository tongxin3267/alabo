using System.Collections.Generic;
using Alabo.App.Share.Rewards.Domain.Entities;
using Alabo.App.Share.Rewards.Dtos;
using Alabo.Domains.Repositories;
using RewardModel = Alabo.App.Share.Rewards.Domain.Entities.Reward;

namespace Alabo.App.Share.Rewards.Domain.Repositories {

    public interface IRewardRepository : IRepository<Reward, long> {

        IList<RewardModel> GetRewardList(RewardInput userInput, out long count);
    }
}