using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SmallTargets.Domain.Repositories {

    public class SmallTargetRankingRepository : RepositoryMongo<SmallTargetRanking, ObjectId>,
        ISmallTargetRankingRepository {

        public SmallTargetRankingRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}