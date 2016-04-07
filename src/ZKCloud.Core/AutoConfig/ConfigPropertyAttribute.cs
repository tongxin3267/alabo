using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Core.AutoConfig {
    public class ConfigPropertyAttribute : Attribute {
        public string Key { get; private set; }

        public ConfigPropertyType Type { get; private set; }

        public ConfigPropertyAttribute(string key, ConfigPropertyType type) {
            Key = key;
            Type = type;
        }
    }
}
