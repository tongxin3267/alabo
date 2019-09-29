using Alabo.Cache;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Alabo.Extensions
{
    /// <summary>
    ///     Type类型扩展
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     换行符
        /// </summary>
        public static string Line => Environment.NewLine;

        /// <summary>
        ///     获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            var type = typeof(T);
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        ///     安全获取值，当值为null时，不会抛出异常
        /// </summary>
        /// <param name="value">可空值</param>
        //public static T SafeValue<T>(this T? value) where T : struct {
        //    return value ?? default(T);
        //}

        /// <summary>
        ///     从缓存中，获取字段属性，加快访问速度
        /// </summary>
        /// <param name="type"></param>
        public static PropertyInfo[] GetPropertiesFromCache(this Type type)
        {
            if (type == null) return null;

            var cacheKey = "GetProperties" + type.FullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out PropertyInfo[] properties))
            {
                properties = type.GetProperties();
                objectCache.Set(cacheKey, properties);
            }

            return properties;
        }

        /// <summary>
        ///     从缓存中，获取字段属性，加快访问速度
        ///     以及相关特性
        /// </summary>
        /// <param name="type"></param>
        public static IList<PropertyResult> GetPropertyResultFromCache(this Type type)
        {
            if (type == null) return null;

            var cacheKey = "GetPropertyResult" + type.FullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out List<PropertyResult> propertyResults))
            {
                var properties = type.GetProperties();
                propertyResults = new List<PropertyResult>();
                foreach (var item in properties)
                {
                    var fieldAttributes = item.GetAttribute<FieldAttribute>();
                    var displayAttribute = item.GetAttribute<DisplayAttribute>();
                    var propertyResult = new PropertyResult
                    {
                        PropertyInfo = item,
                        FieldAttribute = fieldAttributes,
                        DisplayAttribute = displayAttribute
                    };
                    propertyResults.Add(propertyResult);
                }

                objectCache.Set(cacheKey, propertyResults);
            }

            return propertyResults;
        }

        /// <summary>
        ///     转换成类型 返回Type类型
        /// </summary>
        /// <param name="input"></param>
        public static Type GetTypeByFullName(this string input)
        {
            if (input.IsNullOrEmpty()) throw new InvalidExpressionException("类型名称不能为空");

            var cacheKey = "GetTypeByFullName" + input;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out Type type))
            {
                if (input.Length > 110)
                    if (input.Contains("System.Collections.Generic.List"))
                    {
                        // var typefullName = input.CutString("1[[", ", ZKCloud");
                        var findType = Type.GetType(input);
                        if (findType != null)
                        {
                            objectCache.Set(cacheKey, findType);
                            return findType;
                        }
                    }

                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes());
                type = types?.FirstOrDefault(t => t.FullName.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase));
                if (type != null) objectCache.Set(cacheKey, type);

                if (type == null)
                {
                    type = types?.FirstOrDefault(t => t.FullName == input.Trim());
                    if (type != null) objectCache.Set(cacheKey, type);
                }
            }

            //if (type == null) {
            //    throw new InvalidExpressionException("动态获取类型,失败请确定fullName输入是否正确");
            //}
            return type;
        }

        /// <summary>
        ///     转换成类型 返回Type类型
        ///     如果有相同的获取时候，可请使用FullName
        /// </summary>
        /// <param name="input"></param>
        public static Type GetTypeByName(this string input)
        {
            if (input.IsNullOrEmpty()) throw new InvalidExpressionException("类型名称不能为空");

            var objectCache = Ioc.Resolve<IObjectCache>();
            var cacheKey = "GetTypeByName" + input;
            return objectCache.GetOrSetPublic(() =>
            {
                var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies().SelectMany(a => a.GetTypes());
                var type = types?.FirstOrDefault(t =>
                    t.FullName.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase));
                if (type == null)
                    type = types?.FirstOrDefault(t => t.Name.Equals(input.Trim(), StringComparison.OrdinalIgnoreCase));

                //if (type == null) {
                //    throw new InvalidExpressionException("动态获取类型,失败请确定fullName输入是否正确");
                //}
                return type;
            }, cacheKey).Value;
        }

        /// <summary>
        ///     获取示列化
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object GetInstanceByName(this string input)
        {
            var find = input.GetTypeByName();
            if (find == null) return null;

            var cacheKey = "GetInstanceByName" + find.FullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out object config)) config = Activator.CreateInstance(find);

            return config;
        }

        /// <summary>
        ///     获取示列化
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static object GetInstanceByType(this Type type)
        {
            var cacheKey = "GetInstanceByName" + type.FullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out object config)) config = Activator.CreateInstance(type);

            return config;
        }

        /// <summary>
        ///     Type Invoke Mehtod Extension
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static object InvokeMethod(this Type type, string methodName, object[] paras)
        {
            try
            {
                var instance = Activator.CreateInstance(type);
                var rs = type.GetMethod(methodName).Invoke(instance, paras);

                return rs;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///     通过完整的命名空间获取属性值
        /// </summary>
        /// <param name="fullName">输入完整的命名空间</param>
        public static IEnumerable<PropertyDescription> GetAllPropertys(this string fullName)
        {
            try
            {
                var t = fullName.GetTypeByName();
                var configDescription = new ClassDescription(t);
                return configDescription.Propertys.ToList();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     根据完整的命名空间获取 ClassDescription 特性
        /// </summary>
        /// <param name="fullName">完整的命名空间</param>
        public static ClassDescription GetClassDescription(this string fullName)
        {
            var cacheKey = "GetClassDescription" + fullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out ClassDescription configDescription))
            {
                if (fullName.IsNullOrEmpty()) return null;

                var t = fullName.GetTypeByFullName();
                var typeclassProperty = t.GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                configDescription = new ClassDescription(t);
            }

            return configDescription;
        }

        /// <summary>
        ///     根据类型名称或完整命名空间名称，和字段名称，返回字段显示名字
        /// </summary>
        /// <param name="typeName">type name or fullName</param>
        /// <param name="filedName">字段名称</param>
        /// <returns></returns>
        public static string GetFiledDisplayName(this string typeName, string filedName)
        {
            var displayName = string.Empty;
            ;
            var type = typeName.GetTypeByName();
            if (type != null)
            {
                var classDescription = type.FullName.GetClassDescription();
                if (classDescription != null)
                    foreach (var item in classDescription.Propertys)
                        if (item.Property.Name.Equals(filedName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            displayName = item.DisplayAttribute?.Name;
                            if (displayName.IsNullOrEmpty()) displayName = item.Property.Name;

                            break;
                        }
            }

            return displayName;
        }

        /// <summary>
        ///     根据类型名称或完整命名空间名称，和字段名称，获取字段类型
        /// </summary>
        /// <param name="typeName">type name or fullName</param>
        /// <param name="filedName">字段名称</param>
        /// <returns></returns>
        public static Type GetFiledType(this string typeName, string filedName)
        {
            Type displayName = null;
            var type = typeName.GetTypeByName();
            if (type != null)
            {
                var classDescription = type.FullName.GetClassDescription();
                if (classDescription != null)
                    foreach (var item in classDescription.Propertys)
                        if (item.Property.Name.Equals(filedName, StringComparison.CurrentCultureIgnoreCase))
                        {
                            displayName = item.Property.PropertyType;
                            break;
                        }
            }

            return displayName;
        }

        /// <summary>
        ///     根据完整的命名空间获取 AutoDeleteAttribute 特性
        /// </summary>
        /// <param name="fullName">完整的命名空间</param>
        public static AutoDeleteAttribute GetAutoDeleteAttribute(this string fullName)
        {
            var cacheKey = "AutoDeleteAttribute" + fullName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGetPublic(cacheKey, out AutoDeleteAttribute typeclassProperty))
            {
                var t = fullName.GetTypeByFullName();
                typeclassProperty = t.GetTypeInfo().GetAttribute<AutoDeleteAttribute>();
            }

            return typeclassProperty;
        }

        /// <summary>
        ///     根据命名空间获取编辑属性
        /// </summary>
        /// <param name="fullName">输入完整的命名空间</param>
        public static IEnumerable<PropertyDescription> GetEditPropertys(string fullName)
        {
            var propertys = GetAllPropertys(fullName);
            return propertys?.Where(r => r.FieldAttribute.EditShow);
        }

        /// <summary>
        ///     根据命名空间获取列表页属性
        /// </summary>
        /// <param name="fullName">输入完整的命名空间</param>
        public static IEnumerable<PropertyDescription> GetListPropertys(string fullName)
        {
            var propertys = GetAllPropertys(fullName);
            if (propertys != null) return propertys.Where(r => r.FieldAttribute.ListShow);

            return null;
        }
    }

    /// <summary>
    ///     属性结果集
    /// </summary>
    public class PropertyResult
    {
        /// <summary>
        ///     字段特性
        /// </summary>
        public FieldAttribute FieldAttribute { get; set; }

        public DisplayAttribute DisplayAttribute { get; set; }

        /// <summary>
        ///     属性
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
    }
}