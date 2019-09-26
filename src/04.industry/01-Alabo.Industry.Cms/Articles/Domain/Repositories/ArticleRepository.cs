using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories {

    public class ArticleRepository : RepositoryMongo<Article, ObjectId>, IArticleRepository {

        public ArticleRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}