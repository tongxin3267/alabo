using Alabo.Extensions;
using Alabo.Runtime;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Alabo.Apps
{
    /// <summary>
    ///     Class AppCompilerExensions.
    /// </summary>
    public static class AppCompilerExensions
    {
        /// <summary>
        ///     Adds the core reference.
        /// </summary>
        /// <param name="compiler">The compiler.</param>
        public static IDynamicAppCompiler AddCoreReference(this IDynamicAppCompiler compiler)
        {
            if (compiler == null) throw new ArgumentNullException(nameof(compiler));

            var coreLocation = typeof(object).GetTypeInfo().Assembly.Location;
            return compiler.AddReference(coreLocation);
        }

        /// <summary>
        ///     Adds the current references.
        /// </summary>
        /// <param name="compiler">The compiler.</param>
        public static IDynamicAppCompiler AddCurrentReferences(this IDynamicAppCompiler compiler)
        {
            if (compiler == null) throw new ArgumentNullException(nameof(compiler));

            RuntimeContext.Current.GetPlatformRuntimeAssemblies()
                .Foreach(e =>
                {
                    if (File.Exists(e.Location) && !e.Location.EndsWith("ni.dll")) compiler.AddReference(e.Location);
                });
            return compiler;
        }

        /// <summary>
        ///     添加s the default using.
        /// </summary>
        /// <param name="compiler">The compiler.</param>
        public static IDynamicAppCompiler AddDefaultUsing(this IDynamicAppCompiler compiler)
        {
            if (compiler == null) throw new ArgumentNullException(nameof(compiler));

            return compiler.AddUsing("System")
                .AddUsing("System.Linq")
                .AddUsing("System.Collections.Generic");
        }

        /// <summary>
        ///     Adds the directory.
        /// </summary>
        /// <param name="compiler">The compiler.</param>
        /// <param name="path">The path.</param>
        /// <param name="encoding">The encoding.</param>
        public static IDynamicAppCompiler AddDirectory(this IDynamicAppCompiler compiler, string path,
            Encoding encoding)
        {
            if (compiler == null) throw new ArgumentNullException(nameof(compiler));

            if (path == null) throw new ArgumentNullException(nameof(path));

            if (!Directory.Exists(path)) throw new DirectoryNotFoundException($"{path} not found.");

            var directoryInfo = new DirectoryInfo(path);
            var files = directoryInfo.GetFiles();
            compiler.AddFiles(files.Select(e => e.FullName).ToArray(), encoding);
            var directories = directoryInfo.GetDirectories();
            foreach (var item in directories) compiler.AddDirectory(item.FullName, encoding);

            return compiler;
        }

        /// <summary>
        ///     Adds the files.
        /// </summary>
        /// <param name="compiler">The compiler.</param>
        /// <param name="paths">The paths.</param>
        /// <param name="encoding">The encoding.</param>
        public static IDynamicAppCompiler AddFiles(this IDynamicAppCompiler compiler, string[] paths, Encoding encoding)
        {
            if (compiler == null) throw new ArgumentNullException(nameof(compiler));

            if (paths == null) throw new ArgumentNullException(nameof(paths));

            foreach (var path in paths) compiler.AddFile(path, encoding);

            return compiler;
        }
    }
}