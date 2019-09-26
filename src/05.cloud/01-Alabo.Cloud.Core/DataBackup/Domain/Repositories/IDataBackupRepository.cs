using Alabo.Domains.Repositories;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.DataBackup.Domain.Repositories {

    public interface IDataBackupRepository : IRepository<Entities.DataBackup, ObjectId> {
    }
}