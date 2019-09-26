using Alabo.Cloud.Shop.Footprints.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.Shop.Footprints.Domain.Repositories {

    public class FootprintRepository : RepositoryMongo<Footprint, ObjectId>, IFootprintRepository {

        public FootprintRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}