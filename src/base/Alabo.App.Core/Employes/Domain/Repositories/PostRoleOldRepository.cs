using MongoDB.Bson;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Employes.Domain.Repositories {

    public class PostRoleOldRepository : RepositoryMongo<PostRole, ObjectId>, IPostRoleRepository {

        public PostRoleOldRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}