using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Services {

    public interface IProvinceService : IService<Entities.Province, ObjectId> {
    }
}