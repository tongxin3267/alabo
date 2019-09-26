using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Services {

    public interface ICircleService : IService<Entities.Circle, ObjectId> {

        void Init();
    }
}