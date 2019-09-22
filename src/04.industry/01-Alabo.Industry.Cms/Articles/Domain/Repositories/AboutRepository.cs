using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public class AboutRepository : RepositoryMongo<About, ObjectId>, IAboutRepository {

        public AboutRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}