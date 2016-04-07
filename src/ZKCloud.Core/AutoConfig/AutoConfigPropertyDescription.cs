using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZKCloud.Core.AutoConfig.Domain.Entities;
using Newtonsoft.Json;

namespace ZKCloud.Core.AutoConfig {
    public class AutoConfigPropertyDescription {
        public Type ConfigType { get; private set; }

        public string AppName { get; private set; }

        public PropertyInfo Property { get; private set; }

        public ConfigPropertyAttribute PropertyAttribute { get; private set; }

        private Action<IAutoConfig, object> _setValueAction;

        private Func<IAutoConfig, object> _getValueFunction;

        internal AutoConfigPropertyDescription(string appName, Type configType, PropertyInfo property) {
            AppName = appName;
            ConfigType = configType;
            Property = property;
            PropertyAttribute = property.GetCustomAttributes<ConfigPropertyAttribute>().FirstOrDefault();
            if (PropertyAttribute == null)
                PropertyAttribute = new ConfigPropertyAttribute(Property.Name, ConfigPropertyType.Text);
        }

        public object GetValue(IAutoConfig instanse) {
            if (instanse == null)
                throw new ArgumentNullException(nameof(instanse));
            if (_getValueFunction == null) {
                var parameterExpression = Expression.Parameter(typeof(IAutoConfig));
                var convertTypeExpression = Expression.Convert(parameterExpression, ConfigType);
                var propertyExpression = Expression.Property(convertTypeExpression, Property);
                var resultConvertExpression = Expression.Convert(propertyExpression, typeof(object));
                var lambdaExpression = Expression.Lambda<Func<IAutoConfig, object>>(resultConvertExpression, parameterExpression);
                _getValueFunction = lambdaExpression.Compile();
            }
            return _getValueFunction(instanse);
        }

        public void SetValue(IAutoConfig instanse, object value) {
            if (instanse == null)
                throw new ArgumentNullException(nameof(instanse));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (_setValueAction == null) {
                var instanseParameterExpression = Expression.Parameter(typeof(IAutoConfig));
                var valueParameterExpression = Expression.Parameter(typeof(object));
                var convertTypeExpression = Expression.Convert(instanseParameterExpression, ConfigType);
                var convertValueExpression = Expression.Convert(valueParameterExpression, Property.PropertyType);
                var propertyExpression = Expression.Property(convertTypeExpression, Property);
                var propertyAssginExpression = Expression.Assign(propertyExpression, convertValueExpression);
                var lambdaExpression = Expression.Lambda<Action<IAutoConfig, object>>(propertyAssginExpression, instanseParameterExpression, valueParameterExpression);
                _setValueAction = lambdaExpression.Compile();
            }
            _setValueAction(instanse, value);
        }

        public GenericConfig MakeGenericConfig(IAutoConfig instanse) {
            if (instanse == null)
                throw new ArgumentNullException(nameof(instanse));
            GenericConfig result = new GenericConfig() {
                AppName = AppName,
                Key = PropertyAttribute.Key,
                Value = JsonConvert.SerializeObject(GetValue(instanse)),
                LastUpdated = DateTime.Now
            };
            return result;
        }

        public void SetValueFromGenericConfig(IAutoConfig instanse, GenericConfig config) {
            if (instanse == null)
                throw new ArgumentNullException(nameof(instanse));
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            if (config.AppName != AppName || config.Key != PropertyAttribute.Key) {
                throw new ConfigNotMatchException($"config is not for app {AppName} and key {PropertyAttribute.Key}");
            }
            SetValue(instanse, JsonConvert.DeserializeObject(config.Value, Property.PropertyType));
        }
    }
}
