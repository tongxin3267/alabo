using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKCloud.Apps
{
    public interface IDynamicAppCompiler : IDisposable
    {
        string AssemblyName { get; }

        string SavePath { get; }

        IDynamicAppCompiler AddUsing(string usingString);

        IDynamicAppCompiler AddReference(string assemblyPath);

        IDynamicAppCompiler AddSource(string fileName, string sourceCode);

        IDynamicAppCompiler AddFile(string path, Encoding encoding);

        CompilerResult Build();
    }
}
