using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;

namespace Alabo.Data.Things.Goodss.Domain.Repositories
{
    public class GoodsRepository : RepositoryMongo<Goods, long>, IGoodsRepository
    {
        public GoodsRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}