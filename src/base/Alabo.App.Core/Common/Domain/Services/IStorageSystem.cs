using MongoDB.Bson;
using Alabo.App.Core.Common.Domain.Entities;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IStorageSystem {
        string WorkDirectory { get; }

        StorageFile Save(string fileName, byte[] fileBody, bool isManageFile = true);

        StorageFile Save(string fileName, string catalog, byte[] fileBody, bool isManageFile = true);

        StorageFile GetInfo(ObjectId id, bool isAddVisit = false);

        StorageFileContent GetContent(ObjectId id, bool isAddVisit = false);

        void Delete(ObjectId id);
    }
}