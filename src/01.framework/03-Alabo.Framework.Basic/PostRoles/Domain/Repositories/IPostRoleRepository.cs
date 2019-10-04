using Alabo.Domains.Repositories;
using Alabo.Framework.Basic.PostRoles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.PostRoles.Domain.Repositories
{
    public interface IPostRoleRepository : IRepository<PostRole, ObjectId>
    {
    }
}