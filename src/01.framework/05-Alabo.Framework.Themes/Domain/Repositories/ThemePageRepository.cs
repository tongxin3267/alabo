using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Themes.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Domain.Repositories {

    public class ThemePageRepository : RepositoryMongo<ThemePage, ObjectId>, IThemePageRepository {

        public ThemePageRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}