using MongoDB.Bson;
using Alabo.Domains.Services;

namespace Alabo.App.Market.DataBackup.Domain.Services {

    public interface IDataBackupService : IService<Entities.DataBackup, ObjectId> {
    }
}