using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Container;
using ZKCloud.Core.AutoConfig.Domain.Services;

namespace ZKCloud.Core.AutoConfig
{
    public static class AutoConfigManager
    {
        private static IGenericConfigService GetService() {
            return ContainerManager.Default.Resolve<IGenericConfigService>();
        }

        public static AutoConfigDescription GetConfigDescription(Type type) {
            return AutoConfigDescription.Create(type);
        }

        public static void Save(IAutoConfig config) {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            var genericConfigs = GetConfigDescription(config.GetType()).CreateGenericConfigs(config);
            GetService().AddOrUpdate(genericConfigs);
        }

        public static T Get<T>() where T : class, IAutoConfig {
            var description = GetConfigDescription(typeof(T));
            var genericConfigs = GetService().GetList(description.ConfigAttribute.AppName);
            return description.CreateAutoConfig<T>(genericConfigs);
        }
    }
}
