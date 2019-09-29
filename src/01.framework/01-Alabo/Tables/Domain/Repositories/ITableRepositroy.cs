using Alabo.Domains.Repositories;
using Alabo.Tables.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Tables.Domain.Repositories
{
    public interface ITableRepository : IRepository<Table, ObjectId>
    {
    }
}