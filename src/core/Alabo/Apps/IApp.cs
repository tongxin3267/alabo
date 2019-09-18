using System;
using System.Reflection;

/// <summary>
/// </summary>
namespace Alabo.Apps
{
    /// <summary>
    ///     Interface IApp
    /// </summary>
    public interface IApp : IDisposable
    {
        /// <summary>
        ///     Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Gets the application base directory.
        /// </summary>
        string AppBaseDirectory { get; }

        /// <summary>
        ///     Gets the 类型.
        /// </summary>
        AppType Type { get; }

        /// <summary>
        ///     Gets the application assembly.
        /// </summary>
        Assembly AppAssembly { get; }

        /// <summary>
        ///     Initializes the specified application assembly.
        /// </summary>
        /// <param name="appAssembly">The application assembly.</param>
        /// <param name="appBaseDirectory">The application base directory.</param>
        /// <param name="type">The 类型.</param>
        void Initialize(Assembly appAssembly, string appBaseDirectory, AppType type);
    }
}