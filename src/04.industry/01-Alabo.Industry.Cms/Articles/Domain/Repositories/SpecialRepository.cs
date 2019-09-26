using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories {

    public class SpecialRepository : RepositoryMongo<Special, ObjectId>, ISpecialRepository {

        public SpecialRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}