using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Alabo.Runtime
{
    /// <summary>
    ///     Class RuntimePath.
    /// </summary>
    public class RuntimePath
    {
        /// <summary>
        ///     The hosting environment
        /// </summary>
        private readonly IHostingEnvironment _hostingEnvironment;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RuntimePath" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        internal RuntimePath(IHostingEnvironment env)
        {
            _hostingEnvironment = env;
            RootPath = _hostingEnvironment.ContentRootPath;
            WebRootPath = _hostingEnvironment.WebRootPath;
        }

        internal RuntimePath(string rootPath)
        {
            RootPath = rootPath;
            WebRootPath = rootPath + "\\wwwroot";
        }

        /// <summary>
        ///     系统web根目录（绝对路径，如c:\\zkcloud\\wwwroot)
        /// </summary>
        public string BaseDirectory => WebRootPath;

        /// <summary>
        ///     网站的根目录
        /// </summary>
        public string RootPath { get; }

        /// <summary>
        ///     Gets the web root path.
        /// </summary>
        public string WebRootPath { get; }

        /// <summary>
        ///     Gets the application base directory.
        /// </summary>
        public string AppBaseDirectory => Path.Combine(WebRootPath, "apps");

        /// <summary>
        ///     App_Data目录
        /// </summary>
        public string AppDataDirectory => Path.Combine(RootPath, "App_Data");

        /// <summary>
        ///     storage工作目录
        /// </summary>
        public string StorageUploads => Path.Combine(WebRootPath, "Uploads");

        /// <summary>
        ///     日志文件目录
        /// </summary>
        public string LogsDirectory => Path.Combine(AppDataDirectory, "Logs");

        /// <summary>
        ///     网站配置文件路径
        /// </summary>
        public string WebsiteConfigPath => Path.Combine(AppDataDirectory, "config.json");

        /// <summary>
        ///     appsettings 配置文件
        /// </summary>
        public string AppSettings => Path.Combine(RootPath, "appsettings.json");

        /// <summary>
        ///     模块文件路径
        /// </summary>
        public string ComponentsDirectory => Path.Combine(AppDataDirectory, "Components");

        /// <summary>
        ///     模块包路径
        /// </summary>
        public string PackagesDirectory => Path.Combine(AppDataDirectory, "Packages");

        /// <summary>
        ///     Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        public string Combine(string path1)
        {
            return Path.Combine(BaseDirectory, path1);
        }

        /// <summary>
        ///     Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        public string Combine(string path1, string path2)
        {
            return Path.Combine(BaseDirectory, path1, path2);
        }

        /// <summary>
        ///     Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        public string Combine(params string[] path1)
        {
            var fullPathArray = new string[path1.Length + 1];
            fullPathArray[0] = BaseDirectory;
            for (var i = 0; i < path1.Length; i++) {
                fullPathArray[i + 1] = path1[i];
            }

            return Path.Combine(fullPathArray);
        }

        /// <summary>
        ///     Builds the web URL.
        /// </summary>
        /// <param name="path">The path.</param>
        public string BuildWebUrl(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) {
                return RuntimeContext.Current.WebsiteConfig.WebRootUrl;
            }

            path = path.Replace('\\', '/');
            while (path.StartsWith("/")) {
                path = path.Substring(1);
            }

            return RuntimeContext.Current.WebsiteConfig.WebRootUrl + path;
        }
    }
}