using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Employes.Domain.Repositories {

    public class PostRoleRepository : RepositoryMongo<PostRole, ObjectId>, IPostRoleRepository {

        public PostRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}