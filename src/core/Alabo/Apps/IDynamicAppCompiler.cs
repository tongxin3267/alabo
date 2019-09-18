using System;
using System.Text;

/// <summary>
/// </summary>
namespace Alabo.Apps
{
    /// <summary>
    ///     Interface IDynamicAppCompiler
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public interface IDynamicAppCompiler : IDisposable
    {
        /// <summary>
        ///     Gets the name of the assembly.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        ///     Gets the save path.
        /// </summary>
        string SavePath { get; }

        /// <summary>
        ///     Adds the using.
        /// </summary>
        /// <param name="usingString">The using string.</param>
        IDynamicAppCompiler AddUsing(string usingString);

        /// <summary>
        ///     Adds the reference.
        /// </summary>
        /// <param name="assemblyPath">The assembly path.</param>
        IDynamicAppCompiler AddReference(string assemblyPath);

        /// <summary>
        ///     Adds the source.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="sourceCode">The source code.</param>
        IDynamicAppCompiler AddSource(string fileName, string sourceCode);

        /// <summary>
        ///     Adds the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="encoding">The encoding.</param>
        IDynamicAppCompiler AddFile(string path, Encoding encoding);

        /// <summary>
        ///     Builds this instance.
        /// </summary>
        CompilerResult Build();
    }
}