using Alabo.Data.People.Circles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.People.Circles.Domain.Repositories
{
    public class CircleRepository : RepositoryMongo<Circle, ObjectId>, ICircleRepository
    {
        public CircleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}