using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Market.SmallTargets.Domain.Services {

    public class SmallTargetRankingService : ServiceBase<SmallTargetRanking, ObjectId>, ISmallTargetRankingService {

        public SmallTargetRankingService(IUnitOfWork unitOfWork, IRepository<SmallTargetRanking, ObjectId> repository) :
            base(unitOfWork, repository) {
        }
    }
}