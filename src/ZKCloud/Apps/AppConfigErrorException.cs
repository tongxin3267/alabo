using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Apps
{
    public class AppConfigErrorException : Exception
    {
        public AppConfigErrorException(string message)
            : base(message) { }
    }
}
