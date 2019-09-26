using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Provinces.Domain.Repositories {

    public interface IProvinceRepository : IRepository<Entities.Province, ObjectId> {
    }
}