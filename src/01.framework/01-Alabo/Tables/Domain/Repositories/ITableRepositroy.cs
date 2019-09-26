using MongoDB.Bson;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;

namespace Alabo.Domains.Base.Repositories
{
    public interface ITableRepository : IRepository<Table, ObjectId>
    {
    }
}