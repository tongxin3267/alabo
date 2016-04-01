using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Apps
{
    public class CompilerResult
    {
        public bool Success { get; private set; }

        public string Message { get; private set; }

        internal CompilerResult(bool success, string message) {
            Success = success;
            Message = message;
        }
    }
}
