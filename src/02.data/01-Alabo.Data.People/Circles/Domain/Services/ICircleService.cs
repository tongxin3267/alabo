using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Services
{
    public interface ICircleService : IService<Circle, ObjectId>
    {
        void Init();
    }
}