using Alabo.Extensions;
using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Alabo.Mapping.Dynamic {

    /// <summary>
    ///     动态赋值
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Dynamic<TModel> : DynamicObject where TModel : class {
        private readonly IDictionary<PropertyInfo, object> _changedProperties = new Dictionary<PropertyInfo, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            var propertys = typeof(TModel).GetProperties();
            var propertyInfo =
                propertys?.FirstOrDefault(r => r.Name.Equals(binder.Name, StringComparison.OrdinalIgnoreCase));
            // 值为null 不更新
            if (propertyInfo != null && value != null) {
                if (!(propertyInfo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                      propertyInfo.Name.Equals("CreateTime", StringComparison.OrdinalIgnoreCase))) {
                    //如果字段没有忽略则更新
                    var isIgnoredPropery = propertyInfo.GetAttribute<DynamicIgnoreAttribute>() != null;
                    if (!isIgnoredPropery) {
                        if (!value.IsNullOrEmpty()) {
                            _changedProperties.Add(propertyInfo, value);
                        } else {
                            var isIgnoredIfEmpty = propertyInfo.GetAttribute<DynamicNotIgnoreEmptyAttribute>() != null;
                            if (isIgnoredIfEmpty) {
                                _changedProperties.Add(propertyInfo, value);
                            }
                        }

                        // 如果值为空的时候，可以更新，则设置该值
                    }
                }
            }

            return base.TrySetMember(binder, value);
        }

        /// <summary>
        ///     动态设置属性的值
        /// </summary>
        /// <param name="model"></param>
        public void SetValue(TModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (_changedProperties.Count > 0) {
                var propertys = typeof(TModel).GetProperties();
                // 设置最新的更新时间
                var propertyInfo = propertys?.FirstOrDefault(r =>
                    r.Name.Equals("ModifiedTime", StringComparison.OrdinalIgnoreCase));
                if (propertyInfo != null) {
                    _changedProperties.Add(propertyInfo, DateTime.Now);
                }
            }

            foreach (var property in _changedProperties) {
                if (!IsExcludedProperty(property.Key.Name)) {
                    var value = ChangeType(property.Value, property.Key.PropertyType);
                    property.Key.SetValue(model, value);
                }
            }
        }

        private static object ChangeType(object value, Type type) {
            try {
                if (type == typeof(Guid)) {
                    return Guid.Parse((string)value);
                }

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                    if (value == null) {
                        return null;
                    }

                    type = Nullable.GetUnderlyingType(type);
                }

                return Convert.ChangeType(value, type ?? throw new ArgumentNullException(nameof(type)));
            } catch {
                return null;
            }
        }

        private static bool IsExcludedProperty(string propertyName) {
            IEnumerable<string> defaultExcludedProperies = new[] { "ID" };
            return defaultExcludedProperies.Contains(propertyName.ToUpper());
        }
    }
}