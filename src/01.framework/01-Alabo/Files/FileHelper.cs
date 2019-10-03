using Alabo.Domains.Entities;
using Alabo.Runtime;
using System;
using System.Collections.Generic;
using System.IO;

namespace Alabo.Files
{
    /// <summary>
    ///     文件操作类
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        ///     网站根目录
        /// </summary>
        public static string RootPath => RuntimeContext.Current.Path.RootPath;

        /// <summary>
        ///     ZkWeb项目路径
        /// </summary>
        public static string ZkWebPath => RootPath.Replace("Alabo.Test", "").Replace("test", "")
            .Replace("Alabo.Web", "").Replace("web\\", "\\").Replace("zkcloudopen_v2", "zkweb_v1")
            .Replace("diy.5ug.com", "zkweb\\src");

        public static string ZkPcPath => RootPath.Replace("Alabo.Test", "").Replace("test", "")
            .Replace("Alabo.Web", "").Replace("web\\", "\\").Replace("zkcloudopen_v2", "zkpc")
            .Replace("diy.5ug.com", "zkpc\\src");

        public static string ZkOpenDiyPath => RootPath.Replace("Alabo.Test", "").Replace("test", "")
            .Replace("Alabo.Web", "").Replace("web\\", "\\").Replace("zkcloudopen_v2", "zkopen_diy")
            .Replace("diy.5ug.com", "zkweb\\src");

        /// <summary>
        ///     App路径的Path
        /// </summary>
        public static string ZkCloudAppPath => RootPath.Replace("Alabo.Test", "").Replace("test", "app")
            .Replace("zkcloudopen_v2", "zkcloud_v12.8");

        /// <summary>
        ///     Widget的路径
        /// </summary>
        public static string WidgetPath => Path.Combine(RootPath, "Widgets");

        /// <summary>
        ///     WWWroot路径
        /// </summary>
        public static string WwwRootPath => Path.Combine(RootPath, "wwwroot");

        /// <summary>
        ///     二维码图片地址
        /// </summary>
        public static string QrcodePath => Path.Combine(WwwRootPath, "qrcode");

        /// <summary>
        ///     文件上传路径，所有的文件都上传到这个路径上面来
        ///     /wwwroot/uploads
        /// </summary>
        public static string UploadPath => Path.Combine(WwwRootPath, "uploads");

        /// <summary>
        ///     获取图片地址
        /// </summary>
        public static string GetSmallPic(string imageUrl)
        {
            if (imageUrl != null)
            {
                if (File.Exists(RootPath + imageUrl)) {
                    return imageUrl;
                }
            }
            else
            {
                return "/wwwroot/static/images/nopic.png";
            }

            return "/wwwroot/static/images/nopic.png";
        }

        /// <summary>
        ///     创建文件夹
        /// </summary>
        /// <param name="filePath"></param>
        public static ServiceResult CreateDirectory(string filePath)
        {
            try
            {
                if (!Directory.Exists(filePath))
                {
                    var di = Directory.CreateDirectory(filePath);
                }

                return ServiceResult.Success;
            }
            catch (Exception ex)
            {
                return ServiceResult.FailedWithMessage(ex.Message);
            }
        }

        /// <summary>
        ///     根据路径写入文件
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        /// <param name="content">需要写入文件的内容</param>
        public static ServiceResult Write(string path, string content)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(content);
                    return ServiceResult.Success;
                }
            }
        }

        /// <summary>
        ///     根据路径写入文件
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        /// <param name="content">需要写入文件的内容</param>
        public static ServiceResult Write(string path, byte[] content)
        {
            using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var sw = new BinaryWriter(fs))
                {
                    sw.Write(content);
                    return ServiceResult.Success;
                }
            }
        }

        /// <summary>
        ///     Checks and deletes given file if it does exists.
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        public static void DeleteIfExists(string filePath)
        {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }

        public static ServiceResult FileSave(string filePath, string fileContext)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    fs.SetLength(0);
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.WriteAsync(fileContext);
                    }
                }
            }
            catch (Exception)
            {
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     读取文件的内容
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        public static Stream ReadStream(string path)
        {
            if (File.Exists(path))
            {
                Stream stream = new FileStream(path, FileMode.Open);
                return stream;
            }

            return null;
        }

        /// <summary>
        ///     读取文件的内容
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        /// <param name="encoding"></param>
        public static string Read(string path)
        {
            if (File.Exists(path)) {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new StreamReader(fs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        ///     读取文件的内容
        /// </summary>
        /// <param name="path">文件的完整路径</param>
        /// <param name="encoding"></param>
        public static byte[] ReadBuffer(string path)
        {
            if (File.Exists(path)) {
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var sr = new BinaryReader(fs))
                    {
                        var length = fs.Length;
                        var size = 1024;
                        int nTemp, nRead = 0;
                        byte[] buffer;
                        var bufferRes = new byte[length];
                        do
                        {
                            buffer = new byte[size];
                            nTemp = sr.Read(buffer, 0, size);
                            Array.Copy(buffer, 0, bufferRes, nRead, nTemp); // buffer.CopyTo(bufferRes, nRead);
                            nRead += nTemp;
                        } while (nTemp > 0);

                        return bufferRes;
                    }
                }
            }

            return null;
        }

        public static string ReadUrl(string path)
        {
            using (var fs = new FileStream("http://www.baidu.com", FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static Dictionary<string, string> GetFileList(string filePath = "Widgets\\Shared\\Area\\images")
        {
            var dic = new Dictionary<string, string>();
            filePath = Path.Combine(RootPath, filePath);
            if (!Directory.Exists(filePath)) {
                return dic;
            }

            var di = new DirectoryInfo(filePath);
            var filelist = di.GetFiles();

            foreach (var fi in filelist) {
                dic.Add(fi.Name.Substring(0, fi.Name.IndexOf(".")),
                    $"{di.FullName.Replace(RootPath, string.Empty)}\\{fi.Name}");
            }

            return dic;
        }

        public static void Copy(string source, string target)
        {
            try
            {
                if (!File.Exists(source) || !Directory.Exists(target)) {
                    return;
                }

                File.Copy(source, $"{target}/{Path.GetFileName(source)}", true);
            }
            catch
            {
            }
        }
    }
}