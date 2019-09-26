using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Repositories
{
    public class SinglePageRepository : RepositoryMongo<SinglePage, ObjectId>, ISinglePageRepository
    {
        public SinglePageRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}