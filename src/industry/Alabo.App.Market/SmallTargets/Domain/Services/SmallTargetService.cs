using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Market.SmallTargets.Domain.Services {

    public class SmallTargetService : ServiceBase<SmallTarget, ObjectId>, ISmallTargetService {

        public SmallTargetService(IUnitOfWork unitOfWork, IRepository<SmallTarget, ObjectId> repository) : base(
            unitOfWork, repository) {
        }
    }
}