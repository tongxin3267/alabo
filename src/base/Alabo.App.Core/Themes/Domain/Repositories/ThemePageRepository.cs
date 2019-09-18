using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Themes.Domain.Repositories {

    public class ThemePageRepository : RepositoryMongo<ThemePage, ObjectId>, IThemePageRepository {

        public ThemePageRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}