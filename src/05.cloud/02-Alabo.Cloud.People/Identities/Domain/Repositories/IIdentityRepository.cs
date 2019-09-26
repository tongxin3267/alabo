using Alabo.Cloud.People.Identities.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Identities.Domain.Repositories
{
    public interface IIdentityRepository : IRepository<Identity, ObjectId>
    {
    }
}