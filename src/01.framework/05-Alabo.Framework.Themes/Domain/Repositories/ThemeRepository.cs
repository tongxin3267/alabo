using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Framework.Themes.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Domain.Repositories
{
    public class ThemeRepository : RepositoryMongo<Theme, ObjectId>, IThemeRepository
    {
        public ThemeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}