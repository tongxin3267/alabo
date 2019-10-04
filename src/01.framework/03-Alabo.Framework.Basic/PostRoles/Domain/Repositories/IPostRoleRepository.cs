using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Employes.Domain.Repositories
{
    public interface IPostRoleRepository : IRepository<PostRole, ObjectId>
    {
    }
}