using MongoDB.Bson;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Employes.Domain.Repositories {

    public interface IPostRoleRepository : IRepository<PostRole, ObjectId> {
    }
}