using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.App.Core.Themes.Domain.Repositories {

    public class ThemeRepository : RepositoryMongo<Theme, ObjectId>, IThemeRepository {

        public ThemeRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}