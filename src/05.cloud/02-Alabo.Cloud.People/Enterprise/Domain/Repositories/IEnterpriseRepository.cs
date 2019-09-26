using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Cloud.People.Enterprise.Domain.Repositories {

    public interface IEnterpriseRepository : IRepository<Entities.Enterprise, ObjectId> {
    }
}