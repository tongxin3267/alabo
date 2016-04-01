using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace ZKCloud.Apps {
    public abstract class AppBase : IApp {
        public string AppBaseDirectory { get; private set; }

        public virtual string Description { get; private set; }

        public virtual string Name { get; private set; }

        public AppType Type { get; private set; }

        public Assembly AppAssembly { get; private set; }

        public virtual void Dispose() {
        }

        public virtual void Initialize(Assembly appAssembly, string appBaseDirectory, AppType type) {
            AppAssembly = appAssembly;
            AppBaseDirectory = appBaseDirectory;
            Type = type;
            if (type == AppType.Binary)
                InitializeSettings(); //only binary app maybe have json config file, dynamic app use code config
        }

        private void InitializeSettings() {
            string appConfigPath = Path.Combine(AppBaseDirectory, "app.json");
            if (File.Exists(appConfigPath)) {
                //has config file
                using (var fs = new FileStream(appConfigPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                    using (var sr = new StreamReader(fs)) {
                        var appConfig = JsonConvert.DeserializeAnonymousType(sr.ReadToEnd(), new { Name = string.Empty, Description = string.Empty });
                        Name = appConfig.Name;
                        Description = appConfig.Description;
                    }
                }
            } else {
                //no config file, create auto config message
                Name = appConfigPath.Split('\\').Reverse().Skip(2).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(Name))
                    throw new AppConfigErrorException($"can not get app name.");
                Description = Name;
            }
        }
    }
}
