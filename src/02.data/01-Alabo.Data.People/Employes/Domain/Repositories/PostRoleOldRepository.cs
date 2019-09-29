using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Employes.Domain.Repositories
{
    public class PostRoleOldRepository : RepositoryMongo<PostRole, ObjectId>, IPostRoleRepository
    {
        public PostRoleOldRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}