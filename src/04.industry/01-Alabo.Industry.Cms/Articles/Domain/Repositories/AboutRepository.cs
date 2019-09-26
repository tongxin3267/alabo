using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories
{
    public class AboutRepository : RepositoryMongo<About, ObjectId>, IAboutRepository
    {
        public AboutRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}