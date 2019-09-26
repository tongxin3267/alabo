using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Identities.Domain.Repositories {

    public class IdentityRepository : RepositoryMongo<Identity, ObjectId>, IIdentityRepository {

        public IdentityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}