using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Themes.Domain.Repositories {

    public interface IThemeRepository : IRepository<Theme, ObjectId> {
    }
}