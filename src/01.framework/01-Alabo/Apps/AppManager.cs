using Alabo.Extensions;
using Alabo.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Alabo.Apps
{
    /// <summary>
    ///     Class AppManager.
    /// </summary>
    public static class AppManager
    {
        /// <summary>
        ///     The loaded apps
        /// </summary>
        private static readonly IList<IApp> _loadedApps = new List<IApp>();

        /// <summary>
        ///     Gets the loaded apps.
        /// </summary>
        public static IApp[] LoadedApps => _loadedApps.ToArray();

        /// <summary>
        ///     Loads the 所有.
        /// </summary>
        public static void LoadAll()
        {
            var appRootDirectory = new DirectoryInfo(RuntimeContext.Current.Path.AppBaseDirectory);
            _loadedApps.AddRange(
                LoadDynamicApps(appRootDirectory.FullName)); //need add dynamic apps before add binary apps
            var appDirectories = appRootDirectory.GetDirectories();
            foreach (var item in appDirectories)
            {
                var appBinPath = Path.Combine(item.FullName, "bin");
                if (Directory.Exists(appBinPath) && new DirectoryInfo(appBinPath).GetFiles("*.dll").Length > 0) {
                    _loadedApps.AddRange(LoadBinaryApp(item.FullName));
                }
            }
        }

        /// <summary>
        ///     Loads the binary application.
        /// </summary>
        /// <param name="appPath">The application path.</param>
        private static IApp[] LoadBinaryApp(string appPath)
        {
            var appBinPath = Path.Combine(appPath, "bin");
            var dllFiles = new DirectoryInfo(appBinPath).GetFiles("*.dll");
            IList<IApp> appList = new List<IApp>();
            foreach (var file in dllFiles)
            {
                var assembly = AssemblyLoadContext.Default
                    .LoadFromStream(GetAssemblyMemoryStream(file.FullName),
                        GetAssemblySymbolsMemoryStream(file.FullName));
                var appType = assembly.GetTypes()
                    .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract &&
                                typeof(IApp).IsAssignableFrom(e))
                    .FirstOrDefault();
                if (appType == null) // no app cs file, not a app binary
{
                    continue;
                }

                var app = (IApp)Activator.CreateInstance(appType);
                app.Initialize(assembly, appPath, AppType.Binary);
                appList.Add(app);
            }

            return appList.ToArray();
        }

        /// <summary>
        ///     Loads the dynamic apps.
        /// </summary>
        /// <param name="appRootPath">The application root path.</param>
        private static IApp[] LoadDynamicApps(string appRootPath)
        {
            IList<IApp> appList = new List<IApp>();
            var appInterfaceType = typeof(IApp);

            var appTypes = RuntimeContext.Current
                .GetPlatformRuntimeAssemblies()
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType &&
                            appInterfaceType.IsAssignableFrom(e))
                .ToArray();
            foreach (var type in appTypes)
            {
                var app = (IApp)Activator.CreateInstance(type);
                //promise: app namespace eg:Alabo.Web.Apps.Demo01, app name is "Demo01"
                var appName = type.Namespace.Split('.').Last();
                var appPath = Path.Combine(appRootPath, appName);
                app.Initialize(type.GetTypeInfo().Assembly, appPath, AppType.Dynamic);
                appList.Add(app);
            }

            return appList.ToArray();
        }

        /// <summary>
        ///     获取s the assembly memory stream.
        /// </summary>
        /// <param name="path">The path.</param>
        private static Stream GetAssemblyMemoryStream(string path)
        {
            if (!File.Exists(path)) {
                throw new FileNotFoundException();
            }

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
        }

        /// <summary>
        ///     获取s the assembly symbols memory stream.
        /// </summary>
        /// <param name="path">The path.</param>
        private static Stream GetAssemblySymbolsMemoryStream(string path)
        {
            if (!File.Exists(path)) {
                throw new FileNotFoundException();
            }

            var pdbPath = Path.Combine(Path.GetDirectoryName(path), $"{Path.GetFileNameWithoutExtension(path)}.pdb");
            if (!File.Exists(pdbPath)) {
                return null;
            }

            using (var fs = new FileStream(pdbPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                var ms = new MemoryStream();
                fs.CopyTo(ms);
                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                return ms;
            }
        }
    }
}