using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Core.AutoConfig {
    public class ConfigAttribute : Attribute {
        public string AppName { get; private set; }

        public ConfigAttribute(string appName) {
            AppName = appName;
        }
    }
}
