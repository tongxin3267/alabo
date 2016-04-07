using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using ZKCloud.Core.AutoConfig.Domain.Entities;

namespace ZKCloud.Core.AutoConfig
{
    public class AutoConfigDescription
    {
        public Type ConfigType { get; private set; }

        public ConfigAttribute ConfigAttribute { get; private set; }

        public AutoConfigPropertyDescription[] Properties { get; private set; }

        private Func<IAutoConfig> _creator;

        private AutoConfigDescription(Type configType) {
            if (configType == null)
                throw new ArgumentNullException(nameof(configType));
            ConfigType = configType;
            ConfigAttribute = ConfigType.GetTypeInfo().GetCustomAttributes<ConfigAttribute>().FirstOrDefault();
            if (ConfigAttribute == null) {
                string typeName = ConfigType.Name;
                if (typeName.EndsWith("Config"))
                    ConfigAttribute = new ConfigAttribute(typeName.Substring(0, typeName.Length - 6));
                else
                    ConfigAttribute = new ConfigAttribute(typeName);
            }
            Properties = ConfigType
                .GetProperties()
                .Select(e => new AutoConfigPropertyDescription(ConfigAttribute.AppName, e.DeclaringType, e))
                .ToArray();
        }

        public static AutoConfigDescription Create(Type configType) {
            return new AutoConfigDescription(configType);
        }

        public IEnumerable<GenericConfig> CreateGenericConfigs(IAutoConfig config) {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (config.GetType() != ConfigType)
                throw new ArgumentException($"config must with type {ConfigType.Name}");
            IList<GenericConfig> list = new List<GenericConfig>();
            foreach(var item in Properties) {
                list.Add(item.MakeGenericConfig(config));
            }
            return list;
        }

        public IAutoConfig CreateAutoConfig(IEnumerable<GenericConfig> genericConfigs) {
            if (genericConfigs == null)
                throw new ArgumentNullException(nameof(genericConfigs));
            IAutoConfig result = GetCreator()();
            foreach(var item in Properties) {
                var findGenericConfig = genericConfigs.FirstOrDefault(e => e.AppName == item.AppName && e.Key == item.PropertyAttribute.Key);
                if (findGenericConfig != null)
                    item.SetValueFromGenericConfig(result, findGenericConfig);
            }
            return result;
        }

        public T CreateAutoConfig<T>(IEnumerable<GenericConfig> genericConfigs)
            where T : class, IAutoConfig {
            return CreateAutoConfig(genericConfigs) as T;
        }

        private Func<IAutoConfig> GetCreator() {
            if (_creator == null) {
                var newExpression = Expression.New(ConfigType);
                var lambdaExpression = Expression.Lambda<Func<IAutoConfig>>(newExpression);
                _creator = lambdaExpression.Compile();
            }
            return _creator;
        }
    }
}
