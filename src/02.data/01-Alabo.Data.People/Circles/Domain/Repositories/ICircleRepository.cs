using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Repositories
{
    public interface ICircleRepository : IRepository<Circle, ObjectId>
    {
    }
}