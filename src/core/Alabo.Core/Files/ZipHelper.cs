using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Alabo.Domains.Entities;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Runtime;

namespace Alabo.Core.Files {

    public class ZipHelper {
        private static readonly string Packages = RuntimeContext.Current.Path.PackagesDirectory;
        private static readonly string Components = RuntimeContext.Current.Path.ComponentsDirectory;
        private static readonly string Widgets = Path.Combine(RuntimeContext.Current.Path.RootPath, "Widgets");
        private static readonly string Temp = Path.Combine(RuntimeContext.Current.Path.AppDataDirectory, "Temp");
        private static readonly string Tmp = Path.Combine(RuntimeContext.Current.Path.AppDataDirectory, "tmp");

        /// <summary>
        ///     压缩指定文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        public static bool Zip(string path, string fileName) {
            if (path.IsNullOrEmpty()) {
                return false;
            }

            path = path.IndexOf(':') > 0 ? path : $"{Widgets}/{path}";
            if (!Directory.Exists(path)) {
                return false;
            }

            fileName = fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase) ? fileName : $"{fileName}.zip";
            if (File.Exists(fileName)) {
                File.Delete(fileName);
            }

            ZipFile.CreateFromDirectory(path, fileName, CompressionLevel.Optimal, true, Encoding.GetEncoding("GBK"));
            return true;
        }

        /// <summary>
        ///     解压
        /// </summary>
        /// <param name="name">模块包名</param>
        /// <param name="path">模块包中路径参数</param>
        /// <param name="buffer">压缩包数据</param>
        public static bool UnZip(string name, string path, byte[] buffer) {
            var zip = Path.Combine(Packages, $"{name}.zip");
            if (ServiceResult.Success != FileHelper.Write(zip, buffer)) {
                return false;
            }

            var extraTemp = Path.Combine(Temp, Path.GetFileName(path));
            if (Directory.Exists(extraTemp)) {
                Directory.Delete(extraTemp, true);
            }

            ZipFile.ExtractToDirectory(zip, Temp, Encoding.GetEncoding("GBK"));

            CopyTarget(Temp, path);

            Directory.Delete(extraTemp, true);

            return true;
        }

        private static void CopyTarget(string extraTemp, string path) {
            var name = Path.GetFileName(path);
            var bin = Path.Combine(extraTemp, name, "bin");
            var view = Path.Combine(extraTemp, name, "view");

            var dlls = Directory.GetFiles(bin);
            var cshtmls = Directory.GetFiles(view);
            var targetBin = Path.Combine(RuntimeContext.Current.Path.AppDataDirectory, "Components");
            var targetView = Path.Combine(RuntimeContext.Current.Path.RootPath, "Widgets", path);

            Action<string[], string> copy = (files, target) => {
                foreach (var file in files) {
                    FileHelper.Copy(file, target);
                }
            };
            if (!Directory.Exists(targetBin) && ServiceResult.Success != FileHelper.CreateDirectory(targetBin)) {
                throw new ValidException("Copy files to target failed.");
            }

            if (!Directory.Exists(targetView) && ServiceResult.Success != FileHelper.CreateDirectory(targetView)) {
                throw new ValidException("Copy files to target failed.");
            }

            copy(dlls, targetBin);
            copy(cshtmls, targetView);
        }
    }
}