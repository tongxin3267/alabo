﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Alabo.Cache;
using Alabo.Extensions;
using Alabo.Runtime.Config;

namespace Alabo.Runtime {

    /// <summary>
    ///     Class RuntimeContext.
    /// </summary>
    public sealed class RuntimeContext : ICacheConfiguration {

        /// <summary>
        ///     The 服务 provider cache
        /// </summary>
        private static readonly ConcurrentDictionary<int, IServiceProvider> ServiceProviderCache =
            new ConcurrentDictionary<int, IServiceProvider>();

        /// <summary>
        ///     The syn lock
        /// </summary>
        private static readonly object SynLock = new object();

        /// <summary>
        ///     The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        ///     The dependency context
        /// </summary>
        private readonly DependencyContext _dependencyContext = DependencyContext.Load(Assembly.GetEntryAssembly());

        /// <summary>
        /// </summary>
        private string _cacheConfigurationString;

        /// <summary>
        ///     The platform runtime assemblie
        ///     平台程序集
        /// </summary>
        private Assembly[] _platformRuntimeAssemblie;

        /// <summary>
        ///     The runtime assemblies
        ///     程序集
        /// </summary>
        private AssemblyName[] _runtimeAssemblies;

        /// <summary>
        ///     The website configuration
        /// </summary>
        private Lazy<AppSettingConfig> _websiteConfig;

        /// <summary>
        ///     系统环境配置
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        ///     Gets or sets the caching data.
        /// </summary>
        public Dictionary<string, object> CachingData { get; set; } = new Dictionary<string, object>();

        /// <summary>
        ///     Gets the current.
        /// </summary>
        public static RuntimeContext Current { get; } = new RuntimeContext();

        /// <summary>
        ///     当前租户
        /// </summary>
        public static string CurrentTenant { get; set; } = "master";

        /// <summary>
        ///     程序当前版本
        /// </summary>
        public static string Version { get; set; } = "v13";

        /// <summary>
        ///     租户数据库
        ///     宿主数据库名称,在appsetings.json中配置，Mongodb的数据库名称必须和SqlService的数据库名称一样
        /// </summary>
        public static string TenantDataBase => GetTenantDataBase(CurrentTenant);

        /// <summary>
        ///     Gets the path.
        /// </summary>
        public RuntimePath Path { get; internal set; }

        /// <summary>
        ///     Gets the website configuration.
        /// </summary>
        public AppSettingConfig WebsiteConfig {
            get {
                if (_websiteConfig == null) {
                    _websiteConfig = new Lazy<AppSettingConfig>(() => AppSettingConfig.FromConfig(Configuration), true);
                    return _websiteConfig.Value;
                }

                return _websiteConfig.Value;
            }
        }

        /// <summary>
        ///     Gets the request services.
        /// </summary>
        public IServiceProvider RequestServices {
            get {
                var threadId = Thread.CurrentThread.ManagedThreadId;
                ServiceProviderCache.TryGetValue(threadId, out var result);
                return result;
            }
        }

        /// <summary>
        ///     获取当前运行环境下所有程序集
        /// </summary>

        public AssemblyName[] RuntimeAssemblies {
            get {
                if (_runtimeAssemblies == null) {
                    _runtimeAssemblies = _dependencyContext.RuntimeLibraries
                        .SelectMany(library => library.GetDefaultAssemblyNames(_dependencyContext)).ToArray();
                }

                return _runtimeAssemblies;
            }
        }

        /// <summary>
        /// </summary>
        public string CacheConfigurationString {
            get {
                if (string.IsNullOrWhiteSpace(_cacheConfigurationString)) {
                    _cacheConfigurationString = Configuration.GetSection("CacheConfigurationString")?.Value;
                    if (_cacheConfigurationString.IsNullOrEmpty()) {
                        _cacheConfigurationString = "localhost";
                    }
                }

                return _cacheConfigurationString;
            }
            set { }
        }

        /// <summary>
        ///     get tenant database
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public static string GetTenantDataBase(string tenant = "") {
            var database = Current.WebsiteConfig.MongoDbConnection.Database;
            return tenant == "master" || string.IsNullOrWhiteSpace(tenant)
                ? $"{Version}_{database}"
                : $"{Version}_{database}_{tenant}";
        }

        /// <summary>
        ///     获取当前运行环境下符合筛选条件的程序集
        /// </summary>
        /// <param name="predicate">筛选条件</param>
        public Assembly[] GetRuntimeAssemblies(Func<AssemblyName, bool> predicate) {
            if (predicate == null) {
                throw new ArgumentNullException(nameof(predicate));
            }

            var list = RuntimeAssemblies.Where(predicate).Select(Assembly.Load);
            return list.ToArray();
        }

        /// <summary>
        ///     获取当前运行环境下属于ZKCloud平台的程序集
        /// </summary>
        public Assembly[] GetPlatformRuntimeAssemblies() {
            if (_platformRuntimeAssemblie == null) {
                _platformRuntimeAssemblie = GetRuntimeAssemblies(e =>
                    (e.FullName.StartsWith("ZKCloud") || e.FullName.StartsWith("ZKOpen")) &&
                    !e.FullName.Contains("ZKCloud.Open"));
            }

            return _platformRuntimeAssemblie;
        }

        /// <summary>
        ///     Sets the request services.
        /// </summary>
        /// <param name="threadId">The thread identifier.</param>
        /// <param name="serviceProvider">The 服务 provider.</param>
        internal static void SetRequestServices(int threadId, IServiceProvider serviceProvider) {
            ServiceProviderCache.AddOrUpdate(threadId, serviceProvider, (_1, _2) => serviceProvider);
        }

        /// <summary>
        ///     加载所有组件
        /// </summary>
        public void LoadComponents() {
            //return; // 由于只能在请求中获取到applicationPartManager，故无法在启动时加载模块组件
            var path = Current.Path.ComponentsDirectory;
            var files = Directory.GetFiles(path);
            var applicationPartManager = Current.RequestServices.GetService<ApplicationPartManager>();
            if (applicationPartManager == null) {
                return;
            }

            void AddPart(string file) {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                    Assembly assembly = null;
                    assembly = AssemblyLoadContext.Default.LoadFromStream(fs);
                    var part = new AssemblyPart(assembly);

                    applicationPartManager.ApplicationParts.Add(part);
                }
            }

            foreach (var file in files) {
                if (!file.EndsWith(".dll", StringComparison.OrdinalIgnoreCase)) {
                    continue;
                }

                AddPart(file);
            }
        }

        /// <summary>
        ///     加载指定文件
        /// </summary>
        /// <param name="name">模块文件名称</param>
        public void LoadComponent(string name) {
            var path = Current.Path.ComponentsDirectory;
            var fileName = name.EndsWith(".dll") ? name : $"{name}.dll";
            name = name.IndexOf('.') == -1 ? name : name.Substring(0, name.IndexOf('.'));
            var file = System.IO.Path.Combine(path, fileName);
            if (!File.Exists(file)) {
                return;
            }

            var applicationPartManager = Current.RequestServices.GetService<ApplicationPartManager>();
            var viewComponentDescriptorCollectionProvider =
                Current.RequestServices.GetService<IViewComponentDescriptorCollectionProvider>();
            //if (applicationPartManager == null ||
            //    !(viewComponentDescriptorCollectionProvider is ZkCloudViewComponentDescriptorCollectionProvider)
            //) {
            //    return;
            //}

            if (applicationPartManager.ApplicationParts.Any(p => p.Name == name)) {
                return;
            }

            using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read)) {
                Assembly assembly = null;
                assembly = AssemblyLoadContext.Default.LoadFromStream(fs);
                var part = new AssemblyPart(assembly);

                applicationPartManager.ApplicationParts.Add(part);
                //(viewComponentDescriptorCollectionProvider as ZkCloudViewComponentDescriptorCollectionProvider).Reset();
            }
        }
    }
}