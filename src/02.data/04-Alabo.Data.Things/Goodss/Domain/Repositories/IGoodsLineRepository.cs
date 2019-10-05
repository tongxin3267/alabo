using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Data.Things.Goodss.Domain.Repositories
{
    public interface IGoodsLineRepository : IRepository<GoodsLine, ObjectId>
    {
    }
}