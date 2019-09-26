using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Domain.Repositories {

    public class CountyRepository : RepositoryMongo<Domain.Entities.County, ObjectId>, ICountyRepository {

        public CountyRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}