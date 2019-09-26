using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Counties.Domain.Services {

    public interface ICountyService : IService<Domain.Entities.County, ObjectId> {
    }
}