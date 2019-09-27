using Alabo.Cache;
using Alabo.Domains.Services;
using Alabo.Helpers;
using Alabo.Runtime;
using AspectCore.Extensions.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Reflections
{
    /// <summary>
    ///     反射操作
    /// </summary>
    public static class Reflection
    {
        /// <summary>
        ///     获取定义映射配置的程序集列表
        /// </summary>
        public static IList<Assembly> Assemblies => RuntimeContext.Current.GetPlatformRuntimeAssemblies().ToList();

        /// <summary>
        ///     获取实例上的属性值
        /// </summary>
        /// <param name="propertyName">属性值，字段名称</param>
        /// <param name="instance">成员所在的类实例</param>
        public static object GetPropertyValue(this string propertyName, object instance)
        {
            var property = instance.GetType().GetProperty(propertyName);
            if (property == null) return null;

            return GetPropertyValue(property, instance);
        }

        /// <summary>
        ///     设置值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static object SetPropertyValue(this string propertyName, object instance, object value)
        {
            instance.GetType().GetProperty(propertyName).SetValue(instance, value);
            return instance;
        }

        /// <summary>
        ///     获取实例上的属性值
        /// </summary>
        /// <param name="property">属性</param>
        /// <param name="instance">成员所在的类实例</param>
        public static object GetPropertyValue(this PropertyInfo property, object instance)
        {
            if (instance == null) return null;

            if (property != null)
                try
                {
                    var reflector = property.GetReflector();
                    return reflector.GetValue(instance);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            return null;
        }

        /// <summary>
        ///     获取类型描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDescription<T>()
        {
            return GetDescription(Common.GetType<T>());
        }

        /// <summary>
        ///     获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription<T>(string memberName)
        {
            return GetDescription(Common.GetType<T>(), memberName);
        }

        /// <summary>
        ///     获取类型的字段
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static MemberInfo GetMember(Type type, string memberName)
        {
            if (type == null) return null;

            if (string.IsNullOrWhiteSpace(memberName)) return null;

            return type.GetTypeInfo().GetMember(memberName).FirstOrDefault();
        }

        /// <summary>
        ///     获取字段的类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static Type GetMemberType(Type type, string memberName)
        {
            if (type == null) return null;
            var memberInfo = type.GetProperty(memberName);

            if (memberInfo == null) return null;

            return memberInfo.PropertyType;
        }

        /// <summary>
        ///     获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="memberName">成员名称</param>
        public static string GetDescription(Type type, string memberName)
        {
            if (type == null) return string.Empty;

            if (string.IsNullOrWhiteSpace(memberName)) return string.Empty;

            return GetDescription(type.GetTypeInfo().GetMember(memberName).FirstOrDefault());
        }

        /// <summary>
        ///     获取方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static MemberInfo GetMethod(Type type, string memberName)
        {
            var cacheKey = type.FullName + memberName;
            return Ioc.Resolve<IObjectCache>().GetOrSet(() =>
            {
                if (type == null) return null;

                if (string.IsNullOrWhiteSpace(memberName)) return null;

                var methods = type.GetMethods()
                    .Where(e => e.Name.Equals(memberName, StringComparison.OrdinalIgnoreCase));
                if (methods == null || methods.Count() < 0) return null;

                var method = methods.FirstOrDefault();
                return method;
            }, cacheKey).Value;
        }

        /// <summary>
        ///     获取类型成员描述，使用DescriptionAttribute设置描述
        /// </summary>
        /// <param name="member">成员</param>
        public static string GetDescription(MemberInfo member)
        {
            if (member == null) return string.Empty;

            return member.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute
                ? attribute.Description
                : member.Name;
        }

        /// <summary>
        ///     获取显示名称，使用DisplayNameAttribute设置显示名称
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayName<T>()
        {
            return GetDisplayName(Common.GetType<T>());
        }

        /// <summary>
        ///     获取显示名称，使用DisplayAttribute或DisplayNameAttribute设置显示名称
        /// </summary>
        public static string GetDisplayName(MemberInfo member)
        {
            if (member == null) return string.Empty;

            if (member.GetCustomAttribute<DisplayAttribute>() is DisplayAttribute displayAttribute)
                return displayAttribute.Name;

            if (member.GetCustomAttribute<DisplayNameAttribute>() is DisplayNameAttribute displayNameAttribute)
                return displayNameAttribute.DisplayName;

            return string.Empty;
        }

        /// <summary>
        ///     获取显示名称或描述,使用DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static string GetDisplayNameOrDescription<T>()
        {
            return GetDisplayNameOrDescription(Common.GetType<T>());
        }

        /// <summary>
        ///     获取属性显示名称或描述,使用DisplayAttribute或DisplayNameAttribute设置显示名称,使用DescriptionAttribute设置描述
        /// </summary>
        public static string GetDisplayNameOrDescription(MemberInfo member)
        {
            var result = GetDisplayName(member);
            return string.IsNullOrWhiteSpace(result) ? GetDescription(member) : result;
        }

        /// <summary>
        ///     获取所有的服务
        /// </summary>
        public static IEnumerable<Type> GetAllServices()
        {
            var cacheKey = "AllServiceTypes";
            if (!Ioc.Resolve<IObjectCache>().TryGet(cacheKey, out IEnumerable<Type> types))
            {
                var serviceTypes = GetInstancesByInterface<IService>();
                Ioc.Resolve<IObjectCache>().Set(cacheKey, types);
            }

            return types;
        }

        /// <summary>
        ///     获取实现了接口的所有实例
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        public static List<TInterface> GetInstancesByInterface<TInterface>()
        {
            var typeInterface = typeof(TInterface);

            var result = new List<TInterface>();
            foreach (var assembly in Assemblies)
                result.AddRange(assembly.GetTypes()
                    .Where(t => typeInterface.GetTypeInfo().IsAssignableFrom(t) && t != typeInterface &&
                                t.GetTypeInfo().IsAbstract == false)
                    .Select(t => CreateInstance<TInterface>(t)).ToList());

            return result;
        }

        /// <summary>
        ///     动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>
        public static T CreateAutoConfig<T>(Type type, params object[] parameters)
        {
            return Convert.To<T>(Activator.CreateInstance(type, parameters));
        }

        /// <summary>
        ///     动态创建实例
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="type">类型</param>
        /// <param name="parameters">传递给构造函数的参数</param>
        public static T CreateInstance<T>(Type type, params object[] parameters)
        {
            return Convert.To<T>(Activator.CreateInstance(type, parameters));
        }

        /// <summary>
        ///     获取程序集
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        public static Assembly GetAssembly(string assemblyName)
        {
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <summary>
        ///     是否布尔类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsBool(MemberInfo member)
        {
            if (member == null) return false;

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Boolean";

                case MemberTypes.Property:
                    return IsBool((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        ///     是否布尔类型
        /// </summary>
        private static bool IsBool(PropertyInfo property)
        {
            return property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?);
        }

        /// <summary>
        ///     是否枚举类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsEnum(MemberInfo member)
        {
            if (member == null) return false;

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return ((TypeInfo)member).IsEnum;

                case MemberTypes.Property:
                    return IsEnum((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        ///     是否枚举类型
        /// </summary>
        private static bool IsEnum(PropertyInfo property)
        {
            if (property.PropertyType.GetTypeInfo().IsEnum) return true;

            var value = Nullable.GetUnderlyingType(property.PropertyType);
            if (value == null) return false;

            return value.GetTypeInfo().IsEnum;
        }

        /// <summary>
        ///     是否日期类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsDate(MemberInfo member)
        {
            if (member == null) return false;

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.DateTime";

                case MemberTypes.Property:
                    return IsDate((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        ///     是否日期类型
        /// </summary>
        private static bool IsDate(PropertyInfo property)
        {
            if (property.PropertyType == typeof(DateTime)) return true;

            if (property.PropertyType == typeof(DateTime?)) return true;

            return false;
        }

        /// <summary>
        ///     是否整型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsInt(MemberInfo member)
        {
            if (member == null) return false;

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Int32" || member.ToString() == "System.Int16" ||
                           member.ToString() == "System.Int64";

                case MemberTypes.Property:
                    return IsInt((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        ///     是否整型
        /// </summary>
        private static bool IsInt(PropertyInfo property)
        {
            if (property.PropertyType == typeof(int)) return true;

            if (property.PropertyType == typeof(int?)) return true;

            if (property.PropertyType == typeof(short)) return true;

            if (property.PropertyType == typeof(short?)) return true;

            if (property.PropertyType == typeof(long)) return true;

            if (property.PropertyType == typeof(long?)) return true;

            return false;
        }

        /// <summary>
        ///     是否数值类型
        /// </summary>
        /// <param name="member">成员</param>
        public static bool IsNumber(MemberInfo member)
        {
            if (member == null) return false;

            if (IsInt(member)) return true;

            switch (member.MemberType)
            {
                case MemberTypes.TypeInfo:
                    return member.ToString() == "System.Double" || member.ToString() == "System.Decimal" ||
                           member.ToString() == "System.Single";

                case MemberTypes.Property:
                    return IsNumber((PropertyInfo)member);
            }

            return false;
        }

        /// <summary>
        ///     是否数值类型
        /// </summary>
        private static bool IsNumber(PropertyInfo property)
        {
            if (property.PropertyType == typeof(double)) return true;

            if (property.PropertyType == typeof(double?)) return true;

            if (property.PropertyType == typeof(decimal)) return true;

            if (property.PropertyType == typeof(decimal?)) return true;

            if (property.PropertyType == typeof(float)) return true;

            if (property.PropertyType == typeof(float?)) return true;

            return false;
        }

        /// <summary>
        ///     是否集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsCollection(Type type)
        {
            if (type.IsArray) return true;

            return IsGenericCollection(type);
        }

        /// <summary>
        ///     是否泛型集合
        /// </summary>
        /// <param name="type">类型</param>
        public static bool IsGenericCollection(Type type)
        {
            if (!type.IsGenericType) return false;

            var typeDefinition = type.GetGenericTypeDefinition();
            return typeDefinition == typeof(IEnumerable<>)
                   || typeDefinition == typeof(IReadOnlyCollection<>)
                   || typeDefinition == typeof(IReadOnlyList<>)
                   || typeDefinition == typeof(ICollection<>)
                   || typeDefinition == typeof(IList<>)
                   || typeDefinition == typeof(List<>);
        }

        /// <summary>
        ///     从目录中获取所有程序集
        /// </summary>
        /// <param name="directoryPath">目录绝对路径</param>
        public static List<Assembly> GetAssemblies(string directoryPath)
        {
            return Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories).ToList()
                .Where(t => t.EndsWith(".exe") || t.EndsWith(".dll"))
                .Select(path => Assembly.Load(new AssemblyName(path))).ToList();
        }

        /// <summary>
        ///     获取公共属性列表
        /// </summary>
        /// <param name="instance">实例</param>
        public static List<Item> GetPublicProperties(object instance)
        {
            var properties = instance.GetType().GetProperties();
            return properties.ToList().Select(t => new Item(t.Name, t.GetValue(instance))).ToList();
        }

        /// <summary>
        ///     获取顶级基类
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetTopBaseType<T>()
        {
            return GetTopBaseType(typeof(T));
        }

        /// <summary>
        ///     获取顶级基类
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetTopBaseType(Type type)
        {
            if (type == null) return null;

            if (type.IsInterface) return type;

            if (type.BaseType == typeof(object)) return type;

            return GetTopBaseType(type.BaseType);
        }

        /// <summary>
        ///     获取基类中是否包含某个基类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool BaseTypeContains(Type type, Type baseType)
        {
            if (type == null) return false;

            if (type.IsInterface) return false;

            if (type.BaseType == baseType) return true;

            return BaseTypeContains(type.BaseType, baseType);
        }
    }
}