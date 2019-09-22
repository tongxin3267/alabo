using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public class SpecialRepository : RepositoryMongo<Special, ObjectId>, ISpecialRepository {

        public SpecialRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}