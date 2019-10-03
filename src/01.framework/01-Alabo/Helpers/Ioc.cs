using Alabo.Dependency;
using Alabo.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Helpers
{
    /// <summary>
    ///     容器
    /// </summary>
    public static class Ioc
    {
        /// <summary>
        ///     默认容器
        /// </summary>
        internal static readonly Container DefaultContainer = new Container();

        /// <summary>
        ///     创建容器
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public static IContainer CreateContainer(params IConfig[] configs)
        {
            var container = new Container();
            container.Register(null, builder => builder.EnableAop(), configs);
            return container;
        }

        /// <summary>
        ///     创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        public static List<T> ResolveAll<T>(string name = null)
        {
            return DefaultContainer.CreateList<T>(name);
        }

        /// <summary>
        ///     创建集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        public static List<T> ResolveAll<T>(Type type, string name = null)
        {
            return ((IEnumerable<T>)DefaultContainer.CreateList(type, name)).ToList();
        }

        /// <summary>
        ///     创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="name">服务名称</param>
        public static T Resolve<T>(string name = null)
        {
            var scope = GetScope(CurrentScope);
            if (scope != null) {
                return scope.Resolve<T>();
            }

            return DefaultContainer.Create<T>(name);
        }

        /// <summary>
        ///     创建实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="type">对象类型</param>
        /// <param name="name">服务名称</param>
        public static T Resolve<T>(Type type, string name = null)
        {
            return (T)DefaultContainer.Create(type, name);
        }

        /// <summary>
        ///     创建实例
        /// </summary>
        /// <param name="type">创建实例需为接口</param>
        public static object ResolveType(Type type)
        {
            return DefaultContainer.Create(type);
        }

        /// <summary>
        ///     创建实例
        /// </summary>
        /// <param name="fullName">命名空间</param>
        public static object Resolve(string fullName)
        {
            var type = fullName.GetTypeByName();
            if (type != null) {
                return ResolveType(type);
            }

            return null;
        }

        /// <summary>
        ///     作用域开始
        /// </summary>
        public static IScope BeginScope()
        {
            var scope = DefaultContainer.BeginScope();

            AddScope(scope.GetHashCode(), scope);

            return scope;
        }

        /// <summary>
        ///     注册依赖
        /// </summary>
        /// <param name="configs">依赖配置</param>
        public static void Register(params IConfig[] configs)
        {
            DefaultContainer.Register(null, builder => builder.EnableAop(), configs);
        }

        /// <summary>
        ///     注册依赖
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configs">依赖配置</param>
        public static IServiceProvider Register(IServiceCollection services, params IConfig[] configs)
        {
            return DefaultContainer.Register(services, builder => builder.EnableAop(), configs);
        }

        /// <summary>
        ///     释放容器
        /// </summary>
        public static void Dispose()
        {
            DefaultContainer.Dispose();
        }

        #region scope

        [ThreadStatic] public static int CurrentScope;

        /// <summary>
        ///     Tenants
        /// </summary>
        private static readonly ConcurrentDictionary<int, IScope> _scopeDictionaries =
            new ConcurrentDictionary<int, IScope>();

        /// <summary>
        ///     Add scope
        /// </summary>
        /// <param name="hashCode"></param>
        /// <param name="scope"></param>
        public static void AddScope(int hashCode, IScope scope)
        {
            if (!_scopeDictionaries.ContainsKey(hashCode)) {
                _scopeDictionaries.TryAdd(hashCode, scope);
            }
        }

        /// <summary>
        ///     Get scope
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        private static IScope GetScope(int hashCode)
        {
            if (_scopeDictionaries.ContainsKey(hashCode)) {
                return _scopeDictionaries[hashCode];
            }

            return null;
        }

        /// <summary>
        ///     Remove scope
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        public static IScope RemoveScope(int hashCode)
        {
            _scopeDictionaries.TryRemove(hashCode, out var scope);
            return scope;
        }

        #endregion scope
    }
}