using MongoDB.Bson;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Common.Domain.Repositories {

    public class CircleRepository : RepositoryMongo<Circle, ObjectId>, ICircleRepository {

        public CircleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}