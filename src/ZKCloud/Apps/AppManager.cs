using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Runtime;
using ZKCloud.Extensions;

namespace ZKCloud.Apps
{
    public static class AppManager
    {
        private static IList<IApp> _loadedApps = new List<IApp>();

        public static IApp[] LoadedApps { get { return _loadedApps.ToArray(); } }

        public static void LoadAll() {
            DirectoryInfo appRootDirectory = new DirectoryInfo(RuntimeContext.Current.Path.AppBaseDirectory);
            _loadedApps.AddRange(LoadDynamicApps(appRootDirectory.FullName)); //need add dynamic apps before add binary apps
            var appDirectories = appRootDirectory.GetDirectories();
            foreach(var item in appDirectories) {
                string appBinPath = Path.Combine(item.FullName, "bin");
                if(Directory.Exists(appBinPath) && new DirectoryInfo(appBinPath).GetFiles("*.dll").Length > 0) {
                    //if has bin directory and some dll files in bin directory, it's a binary app
                    //else a dynamic app
                    _loadedApps.AddRange(LoadBinaryApp(item.FullName));
                }
            }
        }

        private static IApp[] LoadBinaryApp(string appPath) {
            string appBinPath = Path.Combine(appPath, "bin");
            var dllFiles = new DirectoryInfo(appBinPath).GetFiles("*.dll");
            IList<IApp> appList = new List<IApp>();
            foreach(var file in dllFiles) {
                var assembly = PlatformServices.Default.AssemblyLoadContextAccessor.Default.LoadFile(file.FullName);
                Type appType = assembly.GetTypes()
                    .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && typeof(IApp).IsAssignableFrom(e))
                    .FirstOrDefault();
                if (appType == null) // no app cs file, not a app binary
                    continue;
                IApp app = (IApp)Activator.CreateInstance(appType);
                app.Initialize(assembly, appPath, AppType.Binary);
                appList.Add(app);
            }
            return appList.ToArray();
        }

        private static IApp[] LoadDynamicApps(string appRootPath) {
            IList<IApp> appList = new List<IApp>();
            Type appInterfaceType = typeof(IApp);
            var appTypes = PlatformServices.Default.LibraryManager.GetLibraries()
                .SelectMany(e => e.Assemblies)
                .Where(e => e.Name.StartsWith("ZKCloud"))
                .Distinct()
                .Select(e => Assembly.Load(e))
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType && appInterfaceType.IsAssignableFrom(e))
                .ToArray();
           foreach(Type type in appTypes) {
                IApp app = (IApp)Activator.CreateInstance(type);
                //promise: app namespace eg:ZKCloud.Web.Apps.Demo01, app name is "Demo01"
                string appName = type.Namespace.Split('.').Last();
                string appPath = Path.Combine(appRootPath, appName);
                app.Initialize(type.GetTypeInfo().Assembly, appPath, AppType.Dynamic);
                appList.Add(app);
            }
            return appList.ToArray();
        }
    }
}
