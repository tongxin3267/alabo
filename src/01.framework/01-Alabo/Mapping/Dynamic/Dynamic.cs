using Alabo.Extensions;
using Alabo.Reflections;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Alabo.Mapping.Dynamic {

    /// <summary>
    ///     ��̬��ֵ
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class Dynamic<TModel> : DynamicObject where TModel : class {
        private readonly IDictionary<PropertyInfo, object> _changedProperties = new Dictionary<PropertyInfo, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value) {
            var propertys = typeof(TModel).GetProperties();
            var propertyInfo =
                propertys?.FirstOrDefault(r => r.Name.Equals(binder.Name, StringComparison.OrdinalIgnoreCase));
            // ֵΪnull ������
            if (propertyInfo != null && value != null) {
                if (!(propertyInfo.Name.Equals("Id", StringComparison.OrdinalIgnoreCase) ||
                      propertyInfo.Name.Equals("CreateTime", StringComparison.OrdinalIgnoreCase))) {
                    //����ֶ�û�к��������
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

                        // ���ֵΪ�յ�ʱ�򣬿��Ը��£������ø�ֵ
                    }
                }
            }

            return base.TrySetMember(binder, value);
        }

        /// <summary>
        ///     ��̬�������Ե�ֵ
        /// </summary>
        /// <param name="model"></param>
        public void SetValue(TModel model) {
            if (model == null) {
                throw new ArgumentNullException(nameof(model));
            }

            if (_changedProperties.Count > 0) {
                var propertys = typeof(TModel).GetProperties();
                // �������µĸ���ʱ��
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