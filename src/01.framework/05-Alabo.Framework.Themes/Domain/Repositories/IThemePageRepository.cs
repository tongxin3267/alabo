using Alabo.Domains.Repositories;
using Alabo.Framework.Themes.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Domain.Repositories {

    public interface IThemePageRepository : IRepository<ThemePage, ObjectId> {
    }
}