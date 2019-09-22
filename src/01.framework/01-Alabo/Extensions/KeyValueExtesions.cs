using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Cache;
using Alabo.Domains.Entities;
using Alabo.Helpers;
using Convert = System.Convert;

namespace Alabo.Extensions
{
    /// <summary>
    ///     KeyValueExtesions 扩展
    /// </summary>
    public static class KeyValueExtesions
    {
        /// <summary>
        ///     对象转换成 keyValues
        ///     只显示定义了Field中的特性
        /// </summary>
        /// <param name="instance"></param>
        public static List<KeyValue> ToKeyValues<T>(this T instance)
        {
            if (instance == null) {
                return null;
            }

            var outputType = typeof(T);
            var list = new List<KeyValue>();
            var outputPropertyInfo = outputType.GetPropertyResultFromCache(); //从缓存中读取属性，加快速度
            foreach (var item in outputPropertyInfo) {
                if (item.FieldAttribute != null)
                {
                    var keyValue = new KeyValue
                    {
                        Key = item.PropertyInfo.Name,
                        Name = item.PropertyInfo.Name
                    };
                    // keyValue.Name = item.FieldAttribute.FieldName;
                    if (item.DisplayAttribute != null) {
                        keyValue.Name = item.DisplayAttribute.Name;
                    }

                    keyValue.SortOrder = item.FieldAttribute.SortOrder;

                    keyValue.Value = item.PropertyInfo.GetPropertyValue(instance);
                    // string name=item.PropertyType.Name
                    list.Add(keyValue);
                }
            }

            return list.OrderBy(r => r.SortOrder).ToList();
        }

        /// <summary>
        ///     将枚举转成字典
        /// </summary>
        /// <param name="enumName"></param>
        public static IList<KeyValue> EnumToKeyValues(string enumName)
        {
            var type = enumName.GetTypeByName();
            if (type == null) {
                return null;
            }

            return EnumToKeyValues(type);
        }

        /// <summary>
        ///     将枚举转成字典
        /// </summary>
        /// <param name="enumType"></param>
        public static IList<KeyValue> EnumToKeyValues(Type enumType)
        {
            var objectCache = Ioc.Resolve<IObjectCache>();
            return objectCache.GetOrSet(() =>
                {
                    IList<KeyValue> list = new List<KeyValue>();
                    foreach (var item in Enum.GetValues(enumType))
                    {
                        var value = item.GetDisplayName();
                        var icon = ((Enum) item).GetIcon();
                        var key = Convert.ToInt64(item);
                        var keyValueItem = new KeyValue(key, value, enumType.Name)
                        {
                            Icon = icon
                        };
                        list.Add(keyValueItem);
                    }

                    return list;
                }
                , "EnumToKeyValues_" + enumType.FullName).Value;
        }
    }
}