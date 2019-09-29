using Alabo.Domains.Services;
using Alabo.Framework.Basic.Storages.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Storages.Domain.Services
{
    public interface IStorageFileService : IService<StorageFile, ObjectId>
    {
        /// <summary>
        ///     上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        StorageFile Upload(IFormFileCollection files, string basePath);
    }
}