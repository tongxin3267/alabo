using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Enterprise.Domain.Repositories {

    public class EnterpriseRepository : RepositoryMongo<Entities.Enterprise, ObjectId>, IEnterpriseRepository {

        public EnterpriseRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}