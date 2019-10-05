using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;

namespace Alabo.Data.Things.Goodss.Domain.Services
{
    public class GoodsService : ServiceBase<Goods, long>, IGoodsService
    {
        public GoodsService(IUnitOfWork unitOfWork, IRepository<Goods, long> repository) : base(unitOfWork, repository)
        {
        }
    }
}