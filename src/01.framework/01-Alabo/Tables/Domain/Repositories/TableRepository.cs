using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Tables.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Tables.Domain.Repositories {

    public class TableRepository : RepositoryMongo<Table, ObjectId>, ITableRepository {

        public TableRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}