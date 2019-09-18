using MongoDB.Bson;
using Alabo.Domains.Repositories;

namespace Alabo.App.Market.DataBackup.Domain.Repositories {

    public interface IDataBackupRepository : IRepository<Entities.DataBackup, ObjectId> {
    }
}