using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Repositories {

    public class ProvinceRepository : RepositoryMongo<Entities.Province, ObjectId>, IProvinceRepository {

        public ProvinceRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}