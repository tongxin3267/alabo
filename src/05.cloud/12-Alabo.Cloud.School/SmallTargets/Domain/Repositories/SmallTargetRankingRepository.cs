using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Repositories {

    public class SmallTargetRankingRepository : RepositoryMongo<SmallTargetRanking, ObjectId>,
        ISmallTargetRankingRepository {

        public SmallTargetRankingRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}