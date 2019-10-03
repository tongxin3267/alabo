using System;
using System.IO;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Files;
using Alabo.Framework.Basic.Storages.Domain.Entities;
using Alabo.Helpers;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Storages.Domain.Services
{
    public class StorageFileService : ServiceBase<StorageFile, ObjectId>, IStorageFileService
    {
        public StorageFileService(IUnitOfWork unitOfWork, IRepository<StorageFile, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        /// <summary>
        ///     文件名
        /// </summary>
        /// <param name="files"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public StorageFile Upload(IFormFileCollection files, string basePath)
        {
            var pathBase = $"{basePath.ToLower()}/{DateTime.Now:yyyy-MM-dd}/";
            var uploadPath = Path.Combine(FileHelper.WwwRootPath, pathBase.TrimStart('/')).ToLower().Replace("//", "/");

            if (!Directory.Exists(uploadPath)) {
                Directory.CreateDirectory(uploadPath);
            }

            var uploadFile = files.First();
            var originalName = uploadFile.FileName;
            var storageFile = new StorageFile
            {
                Size = uploadFile.Length,
                Name = originalName,
                Extension = GetFileExt(uploadFile)
            };

            var filename = storageFile.Id + storageFile.Extension;
            using (var fileStream = new FileStream(Path.Combine(uploadPath, filename), FileMode.Create))
            {
                uploadFile.CopyTo(fileStream);
            }

            storageFile.Path = "/wwwroot/" + pathBase + filename;
            storageFile.Path = storageFile.Path.Replace("//", "/");
            Ioc.Resolve<IStorageFileService>().Add(storageFile);
            return storageFile;
        }

        /// <summary>
        ///     获取文件扩展名
        /// </summary>
        private static string GetFileExt(IFormFile uploadFile)
        {
            var temp = uploadFile.FileName.Split('.');
            return "." + temp[temp.Length - 1].ToLower();
        }
    }
}