using MongoDB.Bson;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.User.Domain.Repositories {

    public class IdentityRepository : RepositoryMongo<Identity, ObjectId>, IIdentityRepository {

        public IdentityRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}