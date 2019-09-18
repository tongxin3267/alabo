using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Helpers;
using Alabo.Runtime;
using Convert = System.Convert;
using File = System.IO.File;
using Regex = System.Text.RegularExpressions.Regex;

namespace Alabo.App.Core.Common {

    public class ManagedStorageSystem : IStorageSystem {
        private readonly long _uploadMaxSize;

        public ManagedStorageSystem() {
            WorkDirectory = RuntimeContext.Current.Path.StorageUploads;
            AcceptFiles = RuntimeContext.Current.WebsiteConfig.UploadFiles?.ToLower()
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            _uploadMaxSize = RuntimeContext.Current.WebsiteConfig.UploadMaxSize;
        }

        /// <summary>
        ///     非托管文件上传限制类型，托管型文件允许所有类型
        /// </summary>
        public IList<string> AcceptFiles { get; } = new List<string>();

        public string WorkDirectory { get; }

        public void Delete(ObjectId id) {
            var find = GetService().GetSingle(r => r.Id == id);
            if (find == null) {
                return;
            }

            var pysicalPath = Path.Combine(WorkDirectory, find.Path);
            if (File.Exists(pysicalPath)) {
                File.Delete(pysicalPath);
            }

            GetService().Delete(r => r.Id == id);
        }

        public StorageFile GetInfo(ObjectId id, bool isAddVisit = false) {
            var find = GetService().GetSingle(r => r.Id == id);
            if (find != null && isAddVisit) {
                //  GetService().AddVisit(id);
            }

            return find;
        }

        public StorageFileContent GetContent(ObjectId id, bool isAddVisit = false) {
            var find = GetInfo(id, isAddVisit);
            if (find == null) {
                return null;
            }

            var result = new StorageFileContent {
                CreateTime = find.CreateTime,
                // ExtensionName = find.ExtensionName,
                Id = find.Id,
                //  LastUpdateTime = find.LastUpdateTime,
                Name = find.Name,
                Path = find.Path,
                //   SaveWithExtension = find.SaveWithExtension,
                //  Visits = find.Visits
            };
            var pysicalPath = Path.Combine(WorkDirectory, result.Path);
            if (File.Exists(pysicalPath)) {
                using (var fs = new FileStream(pysicalPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    result.Body = new byte[fs.Length];
                    fs.Read(result.Body, 0, result.Body.Length);
                }
            }

            return result;
        }

        public StorageFile Save(string fileName, byte[] fileBody, bool isManageFile = true) {
            CheckUploadFile(fileName, fileBody.Length, isManageFile);
            var file = new StorageFile {
                Id = ObjectId.GenerateNewId(),
                CreateTime = DateTime.Now,
                // ExtensionName = Path.GetExtension(fileName),
                Name = Path.GetFileNameWithoutExtension(fileName),
                // SaveWithExtension = !isManageFile,
                //  LastUpdateTime = DateTime.Now,
                //  Visits = 0
            };
            //if (!string.IsNullOrWhiteSpace(file.ExtensionName) && file.ExtensionName.StartsWith(".")) {
            //  //  file.ExtensionName = file.ExtensionName.Substring(1);
            //}

            var virtualDirectory = BuildVirtualSaveDirectory();
            var pysicalDirectory = Path.Combine(WorkDirectory, virtualDirectory);
            if (!Directory.Exists(pysicalDirectory)) {
                Directory.CreateDirectory(pysicalDirectory);
            }

            file.Path = $"{virtualDirectory}\\{file.Id.ToString()}";
            //if (!isManageFile) {
            //    file.Path += $".{file.ExtensionName}";
            //}

            var pysicalPath = Path.Combine(WorkDirectory, file.Path);
            using (var fs = new FileStream(pysicalPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)) {
                fs.Write(fileBody, 0, fileBody.Length);
            }

            GetService().Add(file);
            return file;
        }

        public StorageFile Save(string fileName, string catalog, byte[] fileBody, bool isManageFile = true) {
            CheckUploadFile(fileName, fileBody.Length, isManageFile);
            var file = new StorageFile {
                Id = ObjectId.GenerateNewId(),
                CreateTime = DateTime.Now,
                //  ExtensionName = Path.GetExtension(fileName),
                Name = Path.GetFileNameWithoutExtension(fileName),
                // SaveWithExtension = !isManageFile,
                //  LastUpdateTime = DateTime.Now,
                //  Visits = 0
            };
            //if (!string.IsNullOrWhiteSpace(file.ExtensionName) && file.ExtensionName.StartsWith(".")) {
            //  //  file.ExtensionName = file.ExtensionName.Substring(1);
            //}

            var virtualDirectory = string.Empty;
            if (string.IsNullOrEmpty(catalog)) {
                virtualDirectory = BuildVirtualSaveDirectory();
            } else {
                virtualDirectory = BuildVirtualSaveDirectory(catalog);
            }

            var pysicalDirectory = Path.Combine(WorkDirectory, virtualDirectory);
            if (!Directory.Exists(pysicalDirectory)) {
                Directory.CreateDirectory(pysicalDirectory);
            }

            file.Path = $"{virtualDirectory}\\{file.Id.ToString()}";
            if (!isManageFile) {
                // file.Path += $".{file.ExtensionName}";
            }

            var pysicalPath = Path.Combine(WorkDirectory, file.Path);
            using (var fs = new FileStream(pysicalPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)) {
                fs.Write(fileBody, 0, fileBody.Length);
            }

            GetService().Add(file);
            return file;
        }

        public string GetFileUrl(ObjectId id) {
            var find = GetInfo(id, false);
            if (find == null) {
                return null;
            }

            return RuntimeContext.Current.Path.BuildWebUrl(find.Path);
        }

        public StorageFile SaveImage(string fileName, string base64, bool isManageFile = true) {
            var regex = new Regex("^data:image\\/([a-z]*);base64,", RegexOptions.Compiled);
            var match = regex.Match(base64);
            var bytes = Convert.FromBase64String(base64.Substring(match.Value.Length));
            CheckUploadFile(fileName, bytes.Length, isManageFile);
            var file = new StorageFile {
                Id = ObjectId.GenerateNewId(),
                CreateTime = DateTime.Now,
                //    ExtensionName = Path.GetExtension(fileName),
                Name = Path.GetFileNameWithoutExtension(fileName),
                //    SaveWithExtension = !isManageFile,
                //   LastUpdateTime = DateTime.Now,
                //   Visits = 0
            };
            //if (!string.IsNullOrWhiteSpace(file.ExtensionName) && file.ExtensionName.StartsWith(".")) {
            // //   file.ExtensionName = file.ExtensionName.Substring(1);
            //}

            var virtualDirectory = BuildVirtualSaveDirectory();
            var pysicalDirectory = Path.Combine(WorkDirectory, virtualDirectory);
            if (!Directory.Exists(pysicalDirectory)) {
                Directory.CreateDirectory(pysicalDirectory);
            }

            file.Path = $"{virtualDirectory}\\{file.Id.ToString()}";
            if (!isManageFile) {
                //   file.Path += $".{file.ExtensionName}";
            }

            var pysicalPath = Path.Combine(WorkDirectory, file.Path);
            using (var stream = new MemoryStream()) {
                stream.Write(bytes, 0, bytes.Length);
                var image = Image.FromStream(stream);
                image.Save(pysicalPath);
            }

            GetService().Add(file);
            return file;
        }

        public StorageFile SaveImage(string fileName, string catalog, string base64, bool isManageFile = true) {
            var regex = new Regex("^data:image\\/([a-z]*);base64,", RegexOptions.Compiled);
            var match = regex.Match(base64);
            var bytes = Convert.FromBase64String(base64.Substring(match.Value.Length));
            CheckUploadFile(fileName, bytes.Length, isManageFile);
            var file = new StorageFile {
                Id = ObjectId.GenerateNewId(),
                CreateTime = DateTime.Now,
                Extension = Path.GetExtension(fileName),
                //Name = Path.GetFileNameWithoutExtension(fileName),
                //SaveWithExtension = !isManageFile,
                //LastUpdateTime = DateTime.Now,
                //Visits = 0
            };
            //if (!string.IsNullOrWhiteSpace(file.ExtensionName) && file.ExtensionName.StartsWith(".")) {
            //    file.ExtensionName = file.ExtensionName.Substring(1);
            //}

            var virtualDirectory = string.Empty;
            if (string.IsNullOrEmpty(catalog)) {
                virtualDirectory = BuildVirtualSaveDirectory();
            } else {
                virtualDirectory = BuildVirtualSaveDirectory(catalog);
            }

            var pysicalDirectory = Path.Combine(WorkDirectory, virtualDirectory);
            if (!Directory.Exists(pysicalDirectory)) {
                Directory.CreateDirectory(pysicalDirectory);
            }

            file.Path = $"{virtualDirectory}\\{file.Id.ToString()}";
            if (!isManageFile) {
                file.Path += $"{file.Extension}";
            }

            var pysicalPath = Path.Combine(WorkDirectory, file.Path);
            using (var stream = new MemoryStream()) {
                stream.Write(bytes, 0, bytes.Length);
                var image = Image.FromStream(stream);
                image.Save(pysicalPath);
            }

            GetService().Add(file);
            return file;
        }

        /// <summary>
        ///     将图片保存到指定目录下
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="dir">存放的目录</param>
        /// <param name="base64">base64 图片格式</param>
        /// <param name="isManageFile"></param>
        public StorageFile SaveImageToDir(string fileName, string dir, string base64, bool isManageFile = true) {
            var regex = new Regex("^data:image\\/([a-z]*);base64,", RegexOptions.Compiled);
            var match = regex.Match(base64);
            var bytes = Convert.FromBase64String(base64.Substring(match.Value.Length));
            CheckUploadFile(fileName, bytes.Length, isManageFile);
            var file = new StorageFile {
                Id = ObjectId.GenerateNewId(),
                CreateTime = DateTime.Now,
                //ExtensionName = Path.GetExtension(fileName),
                //Name = Path.GetFileNameWithoutExtension(fileName),
                //SaveWithExtension = !isManageFile,
                //LastUpdateTime = DateTime.Now,
                //Visits = 0
            };
            //if (!string.IsNullOrWhiteSpace(file.ExtensionName) && file.ExtensionName.StartsWith(".")) {
            //    file.ExtensionName = file.ExtensionName.Substring(1);
            //}

            var virtualDirectory = string.Empty;
            if (string.IsNullOrWhiteSpace(dir)) {
                virtualDirectory =
                    Path.Combine("uploads\\" + DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
            } else {
                dir = dir.Replace('/', '\\');
                virtualDirectory = Path.Combine(dir.Trim('\\') + '\\' + DateTime.Now.ToString("yyyy"),
                    DateTime.Now.ToString("MM"));
            }

            var pysicalDirectory = Path.Combine(RuntimeContext.Current.Path.RootPath, virtualDirectory);
            if (!Directory.Exists(pysicalDirectory)) {
                Directory.CreateDirectory(pysicalDirectory);
            }

            file.Path = $"{virtualDirectory}\\{file.Id.ToString()}";
            if (!isManageFile) {
                // file.Path += $".{file.ExtensionName}";
            }

            var pysicalPath = Path.Combine(RuntimeContext.Current.Path.RootPath, file.Path);
            using (var stream = new MemoryStream()) {
                stream.Write(bytes, 0, bytes.Length);
                var image = Image.FromStream(stream);
                image.Save(pysicalPath);
            }

            GetService().Add(file);
            return file;
        }

        private string BuildVirtualSaveDirectory(string core) {
            return Path.Combine(core.TrimEnd('\\') + "\\" + DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
        }

        private string BuildVirtualSaveDirectory() {
            return Path.Combine(DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"));
        }

        private string BuildPysicalSaveDirectory() {
            return Path.Combine(WorkDirectory, BuildVirtualSaveDirectory());
        }

        private string BuildVirtualSavePath(Guid id) {
            return $"{BuildVirtualSaveDirectory()}\\{id.ToString()}";
        }

        private string BuildPysicalSavePath(Guid id) {
            return Path.Combine(WorkDirectory, BuildVirtualSavePath(id));
        }

        private static IStorageFileService GetService() {
            return Ioc.Resolve<IStorageFileService>();
        }

        public bool CheckFileSize(long size) {
            return size <= _uploadMaxSize * 1024;
        }

        public bool CheckAcceptFile(string fileName) {
            var extensionName = Path.GetExtension(fileName);
            return AcceptFiles.Any(e => e.Equals(extensionName, StringComparison.OrdinalIgnoreCase));
        }

        public void CheckUploadFile(string fileName, long fileSize, bool isManageFile) {
            if (!CheckFileSize(fileSize)) {
                throw new FileStorageException(fileName, fileSize, $"文件大小超过系统限制({_uploadMaxSize}KB)");
            }

            if (!isManageFile && !CheckAcceptFile(fileName)) {
                throw new FileStorageException(fileName, fileSize, $"不允许上传的文件类型{Path.GetExtension(fileName)}");
            }

            ;
        }
    }

    public class FileStorageException : Exception {

        public FileStorageException(string fileName, long size, string message)
            : base(message) {
            FileName = fileName;
            Size = size;
        }

        public string FileName { get; set; }

        public long Size { get; set; }
    }
}