using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.DataBackup.Domain.Repositories
{
    public class DataBackupRepository : RepositoryMongo<Entities.DataBackup, ObjectId>, IDataBackupRepository
    {
        public DataBackupRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}