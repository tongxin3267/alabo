using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.DataBackup.Domain.Services {

    public class DataBackupService : ServiceBase<Entities.DataBackup, ObjectId>, IDataBackupService {

        public DataBackupService(IUnitOfWork unitOfWork, IRepository<Entities.DataBackup, ObjectId> repository) : base(
            unitOfWork, repository) {
        }
    }
}