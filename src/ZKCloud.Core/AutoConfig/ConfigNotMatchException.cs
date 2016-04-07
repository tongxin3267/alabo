using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Core.AutoConfig {
    public class ConfigNotMatchException : Exception {
        public ConfigNotMatchException(string message)
            : base(message) { }
    }
}
