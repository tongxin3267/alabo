using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.PostRoles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.PostRoles.Domain.Repositories
{
    public class PostRoleRepository : RepositoryMongo<PostRole, ObjectId>, IPostRoleRepository
    {
        public PostRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}