using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories {

    public interface IArticleRepository : IRepository<Article, ObjectId> {
    }
}