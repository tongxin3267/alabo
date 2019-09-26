using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Domain.Repositories {

    public interface ICountyRepository : IRepository<Domain.Entities.County, ObjectId> {
    }
}