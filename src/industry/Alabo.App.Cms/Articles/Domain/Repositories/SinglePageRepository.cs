using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public class SinglePageRepository : RepositoryMongo<SinglePage, ObjectId>, ISinglePageRepository {

        public SinglePageRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}