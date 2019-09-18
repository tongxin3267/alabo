using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SmallTargets.Domain.Repositories {

    public class UserSmallTargetRepository : RepositoryMongo<UserSmallTarget, ObjectId>, IUserSmallTargetRepository {

        public UserSmallTargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}