using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.Address.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Address.Domain.Repositories {

    public class RegionRepository : RepositoryMongo<Region, ObjectId>, IRegionRepository {

        public RegionRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}