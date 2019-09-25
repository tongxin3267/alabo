using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Domains.Entities.Core;
using Alabo.Files;
using Alabo.Runtime;
using Alabo.Test.Base.Core;

namespace Alabo.Test.Generation.CodeTemplate
{
    public static class BaseTemplate
    {
        public static IList<Type> GetMongoEntityTypes()
        {
            var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(IEntity)) ||
                            t.GetInterfaces().Contains(typeof(IMongoEntity))));
            types = types.Where(r => !r.FullName.StartsWith("Alabo.Domain"));
            types = types.Where(r => !r.FullName.Contains("Test."));
            types = types.Where(r => !r.FullName.Contains("Tests."));
            types = types.Where(r => r.IsAbstract == false);
            types = types.Where(r => r.BaseType.FullName.Contains("Mongo"));
            return types.ToList();
        }

        public static string GetFilePath(Type type, string flag, string projectName = "")
        {
            var host = new TestHostingEnvironment();
            var proj = string.Empty;
            if (type.Module.Name == "ZKCloud")
            {
                proj = type.Assembly.GetName().Name + ".Test";
            }
            else
            {
                proj = type.Assembly.GetName().Name.Replace("Entities", "Services").Replace("zkcloudv11s", projectName);
            }

            var paths = type.Namespace.Replace(type.Assembly.GetName().Name, "")
                .Split(".", StringSplitOptions.RemoveEmptyEntries);
            var filePath = $"{host.TestRootPath}\\{proj}".Replace("zkcloudv11s", projectName)
                .Replace("Entities", "Services").Replace("test", "app");
            foreach (var item in paths)
            {
                filePath += "\\" + item.Replace("Entities", flag);
                DirectoryHelper.CreateIfNotExists(filePath);
            }

            return filePath;
        }
    }
}