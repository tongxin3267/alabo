using MongoDB.Bson;
using Alabo.App.Core.Markets.Visitors.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Markets.Visitors.Domain.Repositories {

    public class VisitorRepository : RepositoryMongo<Visitor, ObjectId>, IVisitorRepository {

        public VisitorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}