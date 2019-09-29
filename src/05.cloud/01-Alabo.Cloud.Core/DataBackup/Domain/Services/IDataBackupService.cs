using Alabo.Domains.Services;
using MongoDB.Bson;

namespace _01_Alabo.Cloud.Core.DataBackup.Domain.Services
{
    public interface IDataBackupService : IService<Entities.DataBackup, ObjectId>
    {
    }
}