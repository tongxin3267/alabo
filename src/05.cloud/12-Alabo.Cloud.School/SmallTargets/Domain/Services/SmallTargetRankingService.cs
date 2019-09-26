using Alabo.Cloud.School.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.SmallTargets.Domain.Services
{
    public class SmallTargetRankingService : ServiceBase<SmallTargetRanking, ObjectId>, ISmallTargetRankingService
    {
        public SmallTargetRankingService(IUnitOfWork unitOfWork, IRepository<SmallTargetRanking, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }
    }
}