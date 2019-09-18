using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Cms.Articles.Domain.Repositories {

    public interface IAboutRepository : IRepository<About, ObjectId> {
    }
}