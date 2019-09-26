using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Repositories {

    public interface ICircleRepository : IRepository<Entities.Circle, ObjectId> {
    }
}