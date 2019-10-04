using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Alabo.Apps {

    /// <summary>
    ///     Class AppBase.
    /// </summary>
    public abstract class AppBase : IApp {

        /// <summary>
        ///     Gets the application base directory.
        /// </summary>
        public string AppBaseDirectory { get; private set; }

        /// <summary>
        ///     Gets the description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     Gets the 类型.
        /// </summary>
        public AppType Type { get; private set; }

        /// <summary>
        ///     Gets the application assembly.
        /// </summary>
        public Assembly AppAssembly { get; private set; }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
        }

        /// <summary>
        ///     Initializes the specified application assembly.
        /// </summary>
        /// <param name="appAssembly">The application assembly.</param>
        /// <param name="appBaseDirectory">The application base directory.</param>
        /// <param name="type">The 类型.</param>
        public void Initialize(Assembly appAssembly, string appBaseDirectory, AppType type) {
            AppAssembly = appAssembly;
            AppBaseDirectory = appBaseDirectory;
            Type = type;
            if (type == AppType.Binary) {
                InitializeSettings(); //only binary app maybe have json config file, dynamic app use code config
            }
        }

        /// <summary>
        ///     Initializes the settings.
        /// </summary>
        private void InitializeSettings() {
            var appConfigPath = Path.Combine(AppBaseDirectory, "app.json");
            if (File.Exists(appConfigPath)) {
                //has config file
                using (var fs = new FileStream(appConfigPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    using (var sr = new StreamReader(fs)) {
                        var appConfig = JsonConvert.DeserializeAnonymousType(sr.ReadToEnd(),
                            new { Name = string.Empty, Description = string.Empty });
                        Name = appConfig.Name;
                        Description = appConfig.Description;
                    }
                }
            } else {
                //no config file, create auto config message
                Name = appConfigPath.Split('\\').Reverse().Skip(2).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(Name)) {
                    throw new AppConfigErrorException("can not get app name.");
                }

                Description = Name;
            }
        }
    }
}