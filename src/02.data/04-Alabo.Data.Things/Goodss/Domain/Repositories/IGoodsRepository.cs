using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.Data.Things.Goodss.Domain.Repositories
{
    public interface IGoodsRepository : IRepository<Goods, long>
    {
    }
}