using AspectCore.Extensions.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Alabo.Reflections
{
    /// <summary>
    ///     Class CustomAttribute.
    ///     特性扩展
    /// </summary>
    public static class CustomAttribute
    {
        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
        {
            var reflector = propertyInfo.GetReflector();
            return reflector.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="typeInfo">The property information.</param>
        public static T GetAttribute<T>(this TypeInfo typeInfo) where T : Attribute
        {
            var reflector = typeInfo.GetReflector();
            return reflector.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="typeInfo">The property information.</param>
        public static T GetAttribute<T>(this Type typeInfo) where T : Attribute
        {
            var reflector = typeInfo.GetTypeInfo().GetReflector();
            return reflector.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="methodInfo">The property information.</param>
        public static T GetAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        {
            var reflector = methodInfo.GetReflector();
            return reflector.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static T GetAttribute<T>(this FieldInfo fieldInfo) where T : Attribute
        {
            //var reflector = fieldInfo.GetReflector();
            //return reflector.GetCustomAttribute<T>();
            return fieldInfo.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static T GetAttribute<T>(this ParameterInfo fieldInfo) where T : Attribute
        {
            var reflector = fieldInfo.GetReflector();
            return reflector.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="memberInfo">The property information.</param>
        public static T GetAttribute<T>(this MemberInfo memberInfo) where T : Attribute
        {
            //var propertyInfo = typeof(T).GetProperty(memberInfo.Name);
            //var reflector = propertyInfo.GetReflector();
            //return reflector.GetCustomAttribute<T>();
            return memberInfo.GetCustomAttribute<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static Attribute[] GetAttributes(this PropertyInfo propertyInfo)
        {
            var reflector = propertyInfo.GetReflector();
            return reflector.GetCustomAttributes();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="propertyInfo">The property information.</param>
        public static Attribute[] GetAttributes(this ParameterInfo propertyInfo)
        {
            var reflector = propertyInfo.GetReflector();
            return reflector.GetCustomAttributes();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="typeInfo">The property information.</param>
        public static Attribute[] GetAttributes(this TypeInfo typeInfo)
        {
            var reflector = typeInfo.GetReflector();
            return reflector.GetCustomAttributes();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="methodInfo">The property information.</param>
        public static Attribute[] GetAttributes(this MethodInfo methodInfo)
        {
            var reflector = methodInfo.GetReflector();
            return reflector.GetCustomAttributes();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static Attribute[] GetAttributes(this FieldInfo fieldInfo)
        {
            var reflector = fieldInfo.GetReflector();
            return reflector.GetCustomAttributes();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="memberInfo">The property information.</param>
        public static Attribute[] GetAttributes(this MemberInfo memberInfo)
        {
            return memberInfo.GetCustomAttributes().ToArray();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static IEnumerable<T> GetAttributes<T>(this FieldInfo fieldInfo) where T : Attribute
        {
            return fieldInfo.GetCustomAttributes<T>();
            //var reflector = fieldInfo.GetReflector();
            //return reflector.GetCustomAttributes<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static T[] GetAttributes<T>(this TypeInfo fieldInfo) where T : Attribute
        {
            var reflector = fieldInfo.GetReflector();
            return reflector.GetCustomAttributes<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static T[] GetAttributes<T>(this PropertyInfo fieldInfo) where T : Attribute
        {
            var reflector = fieldInfo.GetReflector();
            return reflector.GetCustomAttributes<T>();
        }

        /// <summary>
        ///     通过AspectCore反射扩展，获取特性，速度更快
        /// </summary>
        /// <param name="fieldInfo">The property information.</param>
        public static T[] GetAttributes<T>(this MemberInfo fieldInfo) where T : Attribute
        {
            //var propertyInfo = typeof(T).GetProperty(fieldInfo.Name);
            //var reflector = propertyInfo.GetReflector();
            //return reflector.GetCustomAttributes<T>();
            return fieldInfo.GetCustomAttributes<T>().ToArray();
        }
    }
}