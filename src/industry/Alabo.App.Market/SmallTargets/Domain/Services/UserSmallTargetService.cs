using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.App.Market.SmallTargets.Domain.Services {

    public class UserSmallTargetService : ServiceBase<UserSmallTarget, ObjectId>, IUserSmallTargetService {

        public UserSmallTargetService(IUnitOfWork unitOfWork, IRepository<UserSmallTarget, ObjectId> repository) : base(
            unitOfWork, repository) {
        }
    }
}