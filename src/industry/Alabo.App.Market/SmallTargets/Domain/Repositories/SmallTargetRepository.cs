using MongoDB.Bson;
using Alabo.App.Market.SmallTargets.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.SmallTargets.Domain.Repositories {

    public class SmallTargetRepository : RepositoryMongo<SmallTarget, ObjectId>, ISmallTargetRepository {

        public SmallTargetRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}