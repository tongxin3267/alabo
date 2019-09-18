using MongoDB.Bson;
using Alabo.App.Open.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Open.Attach.Domain.Repositories {

    public class FootprintRepository : RepositoryMongo<Footprint, ObjectId>, IFootprintRepository {

        public FootprintRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}