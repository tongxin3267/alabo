using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Common.Domain.Services {

    public interface IStorageFileService : IService<StorageFile, ObjectId> {

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="files"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        StorageFile Upload(IFormFileCollection files, string basePath);
    }
}