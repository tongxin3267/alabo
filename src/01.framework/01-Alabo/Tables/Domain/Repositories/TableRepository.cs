using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Entities;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace Alabo.Domains.Base.Repositories
{
    public class TableRepository : RepositoryMongo<Table, ObjectId>, ITableRepository
    {
        public TableRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}