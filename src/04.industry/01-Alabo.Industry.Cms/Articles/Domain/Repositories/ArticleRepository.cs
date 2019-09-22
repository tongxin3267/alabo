using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public class ArticleRepository : RepositoryMongo<Article, ObjectId>, IArticleRepository {

        public ArticleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}