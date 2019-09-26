using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Things.Goodss.Domain.Repositories
{
    public class GoodsLineRepository : RepositoryMongo<GoodsLine, ObjectId>, IGoodsLineRepository
    {
        public GoodsLineRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}