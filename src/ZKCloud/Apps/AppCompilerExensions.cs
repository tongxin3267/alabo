using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.PlatformAbstractions;
using ZKCloud.Extensions;

namespace ZKCloud.Apps
{
    public static class AppCompilerExensions
    {
        public static IDynamicAppCompiler AddFiles(this IDynamicAppCompiler compiler, string[] paths, Encoding encoding) {
            if (compiler == null)
                throw new ArgumentNullException(nameof(compiler));
            if (paths == null)
                throw new ArgumentNullException(nameof(paths));
            foreach(var path in paths) {
                compiler.AddFile(path, encoding);
            }
            return compiler;
        }

        public static IDynamicAppCompiler AddDirectory(this IDynamicAppCompiler compiler, string path, Encoding encoding) {
            if (compiler == null)
                throw new ArgumentNullException(nameof(compiler));
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"{path} not found.");
            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles();
            compiler.AddFiles(files.Select(e => e.FullName).ToArray(), encoding);
            var directories = directoryInfo.GetDirectories();
            foreach(var item in directories) {
                compiler.AddDirectory(item.FullName, encoding);
            }
            return compiler;
        }

        public static IDynamicAppCompiler AddDefaultUsing(this IDynamicAppCompiler compiler) {
            if (compiler == null)
                throw new ArgumentNullException(nameof(compiler));
            return compiler.AddUsing("System")
                .AddUsing("System.Linq")
                .AddUsing("System.Collections.Generic");
        }

        public static IDynamicAppCompiler AddCoreReference(this IDynamicAppCompiler compiler) {
            if (compiler == null)
                throw new ArgumentNullException(nameof(compiler));
            string coreLocation = typeof(object).GetTypeInfo().Assembly.Location;
            return compiler.AddReference(coreLocation);
        }

        public static IDynamicAppCompiler AddCurrentReferences(this IDynamicAppCompiler compiler) {
            if (compiler == null)
                throw new ArgumentNullException(nameof(compiler));
            PlatformServices.Default.LibraryManager.GetLibraries()
                .SelectMany(e => e.Assemblies)
                .Distinct()
                .Select(e => Assembly.Load(e))
                .Foreach(e => {
                    if (File.Exists(e.Location) && !e.Location.EndsWith("ni.dll")) {
                        compiler.AddReference(e.Location);
                    }
                });
            return compiler;
        }

    }
}
