using Alabo.Domains.Entities;
using Alabo.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alabo.Mapping {

    /// <summary>
    ///     自动动态赋值
    /// </summary>
    public static class AutoMapping {

        /// <summary>
        ///     动态设置值，将值赋值出去，对传入的值进行赋值
        ///     通过缓存动态设置值，只要两个属性的名称和类型相同即可
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="instance">传入的对象示例，对传入的值进行赋值，对象不能为空</param>
        public static T SetValue<T>(object data, T instance) {
            if (instance == null) {
                return default;
            }

            if (data == null) {
                return instance;
            }
            // 如果是字典类型
            if (data.GetType().FullName.Contains("System.Collections.Generic.Dictionary")) {
                try {
                    var dic = (Dictionary<string, string>)data;
                    return SetValueDictionary<T>(dic);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

            var outputPropertyInfo = instance.GetType().GetPropertiesFromCache(); //从缓存中读取属性，加快速度

            //输入字符串类型
            //  var inputType = data.ToStr().GetTypeByFullName();
            var inputType = data.GetType();
            if (inputType != null) {
                var inputPropertyInfo = inputType.GetPropertiesFromCache();
                foreach (var item in outputPropertyInfo) {
                    var property = inputPropertyInfo.FirstOrDefault(r =>
                        r.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                    if (property != null) {
                        var value = property.GetValue(data);
                        //值转换
                        SetPropertyInfoValue(instance, item, value);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        ///     动态设置值,重新示例化对象
        ///     通过缓存动态设置值，只要两个属性的名称和类型相同即可
        /// </summary>
        /// <param name="data">The data.</param>
        public static T SetValue<T>(object data) {
            var outputType = typeof(T);
            var instance = (T)outputType.GetInstanceByType();
            return SetValue(data, instance);
        }

        /// <summary>
        ///     动态设置值
        ///     通过缓存动态设置值，只要两个属性的名称和类型相同即可
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        private static T SetValueDictionary<T>(Dictionary<string, string> dictionary) {
            // 输出类型
            var outputType = typeof(T);
            var outputPropertyInfo = outputType.GetPropertiesFromCache(); //从缓存中读取属性，加快速度
            var output = (T)outputType.GetInstanceByType();

            foreach (var item in outputPropertyInfo) {
                try {
                    object value = null;
                    foreach (var dic in dictionary) {
                        if (item.Name.Equals(dic.Key, StringComparison.OrdinalIgnoreCase)) {
                            value = dic.Value;
                            break;
                        }
                    }

                    if (!value.IsNullOrEmpty()) {
                        SetPropertyInfoValue(output, item, value);
                    }
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

            return output;
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="httpContext">The form collection.</param>
        /// <param name="instance">The instance.</param>
        public static object SetValue(HttpContext httpContext, object instance) {
            if (instance == null) {
                return instance;
            }

            var formCollection = httpContext.Request.Form;
            var outputPropertyInfo = instance.GetType().GetPropertiesFromCache(); //从缓存中读取属性，加快速度
            foreach (var item in outputPropertyInfo) {
                if (formCollection.ContainsKey(item.Name)) {
                    // 使用第一个值，解决如果页面上有两个元素时所造成的bug
                    var value = formCollection[item.Name][0];
                    SetPropertyInfoValue(instance, item, value);
                }
            }

            return instance;
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="httpContext">The form collection.</param>
        /// <param name="instance">The instance.</param>
        public static T SetValue<T>(HttpContext httpContext, T instance) {
            if (instance == null) {
                return default;
            }

            var formCollection = httpContext.Request.Form;
            var outputPropertyInfo = instance.GetType().GetPropertiesFromCache(); //从缓存中读取属性，加快速度
            foreach (var item in outputPropertyInfo) {
                if (formCollection.ContainsKey(item.Name)) {
                    try {
                        var value = formCollection[item.Name];
                        SetPropertyInfoValue(instance, item, value);
                    } catch (Exception ex) {
                        if (item.PropertyType == typeof(bool) || item.PropertyType == typeof(bool)) {
                            var value = formCollection[item.Name];
                            // swtich表单异常
                            if (value.ToString() == "on") {
                                SetPropertyInfoValue(instance, item, true);
                            } else {
                                SetPropertyInfoValue(instance, item, false);
                            }
                        }

                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        ///     通过表单 动态设置值
        ///     通过缓存动态设置值，只要两个属性的名称和类型相同即可
        /// </summary>
        /// <param name="httpContext">The form collection.</param>
        public static T SetValue<T>(HttpContext httpContext) {
            var outputType = typeof(T);
            var outputPropertyInfo = outputType.GetPropertiesFromCache(); //从缓存中读取属性，加快速度
            var output = (T)outputType.GetInstanceByType();

            return SetValue(httpContext, output);
        }

        /// <summary>
        ///     通过表单 动态设置值
        ///     通过缓存动态设置值，只要两个属性的名称和类型相同即可
        /// </summary>
        /// <param name="formCollection">The form collection.</param>
        /// <param name="type">The 类型.</param>
        public static object SetValue(IFormCollection formCollection, Type type) {
            var output = type.GetInstanceByType();
            return SetValue(formCollection, output);
        }

        /// <summary>
        ///     设置属性值
        ///     c
        /// </summary>
        /// <param name="instance">需要值处理的实例对象，不能为空</param>
        /// <param name="propertyInfo">字段属性</param>
        /// <param name="value">值，为空时不处理</param>
        public static T SetPropertyInfoValue<T>(T instance, PropertyInfo propertyInfo, object value) {
            if (value == null || propertyInfo == null) {
                return instance;
            }
            // 序列号不映射
            if (propertyInfo.Name == "Serial") {
                return instance;
            }

            if (propertyInfo.PropertyType == typeof(string)) {
                propertyInfo.SetValue(instance, value.ToStr());
            } else if (propertyInfo.PropertyType == typeof(int)) {
                propertyInfo.SetValue(instance, Convert.ToInt32(value.ToStr()));
            } else if (propertyInfo.PropertyType == typeof(decimal)) {
                propertyInfo.SetValue(instance, Convert.ToDecimal(value.ToStr()));
            } else if (propertyInfo.PropertyType == typeof(long)) {
                propertyInfo.SetValue(instance, Convert.ToInt64(value.ToStr()));
            } else if (propertyInfo.PropertyType == typeof(DateTime)) {
                propertyInfo.SetValue(instance, Convert.ToDateTime(value.ToStr()));
            } else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool)) {
                propertyInfo.SetValue(instance, Convert.ToBoolean(value.ToStr()));
            } else if (propertyInfo.PropertyType == typeof(Guid)) {
                propertyInfo.SetValue(instance, Guid.Parse(value.ToStr()));
            } else if (propertyInfo.PropertyType.GetTypeInfo().BaseType?.Name == nameof(Enum)) {
                try {
                    var enumValue = Enum.Parse(propertyInfo.PropertyType.GetTypeInfo().UnderlyingSystemType,
                        value.IsNullOrEmpty() ? "0" : value.ToStr(), true);
                    propertyInfo.SetValue(instance, enumValue);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            } else if (propertyInfo.Name == "HttpContext") {
            } else {
                try {
                    // 类型不转换
                    // 改方法还存在类型转换不成功的情况，请逐步补充
                    propertyInfo.SetValue(instance, value);
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                    // 此处不能转换成String对象，不能value.ToStr()
                    try {
                        propertyInfo.SetValue(instance, Convert.ChangeType(value.ToStr(), propertyInfo.PropertyType));
                    } catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        ///     类型转换,转换成Html内容
        ///     如果转换成功，则返回
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="type">The 类型.</param>
        public static Tuple<bool, object> TryChangeHtmlValue(object value, Type type) {
            var changeValue = value;
            if (value == null || value.ToStr().IsNullOrEmpty()) {
                return Tuple.Create(false, changeValue);
            }

            if (type == typeof(string)) {
                changeValue = value.ToStr();
                if (!changeValue.IsNullOrEmpty()) {
                    return Tuple.Create(true, changeValue);
                }
            } else if (type == typeof(int)) {
                if (value.ToStr().ConvertToInt() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(decimal)) {
                if (value.ToStr().ConvertToDecimal() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(decimal?)) {
                if (value.ToStr().ConvertToDecimal() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(long)) {
                if (value.ToStr().ConvertToLong() != -1) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(DateTime)) {
                if (value.ToStr().ConvertToDateTime().Year != 1900) {
                    return Tuple.Create(true, value);
                }
            } else if (type == typeof(bool) || type == typeof(bool)) {
                var valueDefault = value.ConvertToNullableBool();
                changeValue = (bool)value ? "是" : "否";
                if (valueDefault.HasValue) {
                    return Tuple.Create(true, changeValue);
                }
            } else if (type == typeof(Guid)) {
                if (!value.ToGuid().IsGuidNullOrEmpty()) {
                    return Tuple.Create(true, value);
                }
            } else if (type.GetTypeInfo().BaseType?.Name == nameof(Enum)) {
                var enumValue = Enum.Parse(type.GetTypeInfo().UnderlyingSystemType,
                    value.IsNullOrEmpty() ? "0" : value.ToStr(), true);
                enumValue = enumValue.GetHtmlName();
                return Tuple.Create(true, enumValue);
            } else {
                return Tuple.Create(false, changeValue);
            }

            return Tuple.Create(false, changeValue);
        }

        /// <summary>
        ///     转换为字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static Dictionary<string, string> ConverDictionary<T>(T instance) {
            return null;
        }

        /// <summary>
        ///     PageList对象转换
        /// </summary>
        /// <param name="dataSource">数据源，必须为PagedList对象</param>
        public static PagedList<T> ConverPageList<T>(object dataSource) {
            var resultList = new PagedList<T>();
            // 数据传入对象非PagedList对象
            if (!dataSource.GetType().FullName.Contains("Alabo.Domains.Entities.PagedList")) {
                return null;
            }

            var pagedList = (dynamic)dataSource;
            foreach (var item in pagedList) {
                T result = SetValue<T>(item);
                resultList.Add(result);
            }

            return PagedList<T>.Create(resultList, pagedList.RecordCount, pagedList.PageSize, pagedList.PageIndex);
        }

        /// <summary>
        ///     Convers the 分页 list.
        /// </summary>
        /// <param name="pagedList">The paged list.</param>
        public static PagedList<TOutput> ConverPageList<TOutput, TInput>(PagedList<TInput> pagedList) {
            var resultList = new PagedList<TOutput>();
            foreach (var item in pagedList) {
                var instance = SetValue<TOutput>(item);
                resultList.Add(instance);
            }

            return PagedList<TOutput>.Create(resultList, pagedList.RecordCount, pagedList.PageSize,
                pagedList.PageIndex);
        }
    }
}