using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ZKCloud.Apps {
    public interface IApp : IDisposable {
        string Name { get; }

        string Description { get; }

        string AppBaseDirectory { get; }

        AppType Type { get; }

        Assembly AppAssembly { get; }

        void Initialize(Assembly appAssembly, string appBaseDirectory, AppType type);
    }
}
