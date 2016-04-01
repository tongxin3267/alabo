using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ZKCloud.Apps;
using ZKCloud.Extensions;

namespace ZKCloud.Web.Mvc
{
    public static class AppExtensions
    {
        public static void AddAppControllerTypes(IList<TypeInfo> list) {
            var binaryApps = AppManager.LoadedApps.Where(e => e.Type == AppType.Binary).ToArray(); //mvc will auto add dynamic app controllers
            foreach (var app in binaryApps) {
                var controllerTypes = app.AppAssembly
                    .GetTypes()
                    .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && typeof(Controller).IsAssignableFrom(e))
                    .Select(e => e.GetTypeInfo())
                    .ToArray();
                list.AddRange(controllerTypes);
            }
        }
    }
}
