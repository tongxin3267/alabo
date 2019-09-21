﻿using System.IO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Alabo.Domains.Repositories.Mongo.Context;
using Alabo.Extensions;

namespace Alabo.Runtime.Config {

    /// <summary>
    ///     系统配置
    /// </summary>
    public class AppSettingConfig {
        private readonly IConfiguration _systemConfiguration;

        private string _clientHost;

        private string _connectionString;

        private MongoDbConnection _mongoDbConnection;

        /// <summary>
        /// </summary>
        /// <param name="systemConfiguration"></param>
        public AppSettingConfig(IConfiguration systemConfiguration) {
            _systemConfiguration = systemConfiguration;
        }

        private AppSettingConfig() {
        }

        /// <summary>
        ///     Gets or sets the cache scheme.
        ///     缓存模式，默认为内存缓存
        ///     支持redis缓存 redis
        ///     值为：redis或memory
        /// </summary>
        public string CacheScheme {
            get {
                var value = "memory";
                var setting = _systemConfiguration.GetSection("CacheScheme");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        ///     使用的数据库
        ///     可以指定这些值
        ///     postgresql, sqlite, mysql, mssql
        /// </summary>
        public string Database {
            get {
                var value = "mssql";
                var setting = _systemConfiguration.GetSection("Database");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        ///     iis访问站点
        /// </summary>
        public string ClientHost {
            get {
                if (_clientHost.IsNullOrEmpty()) {
                    var setting = _systemConfiguration.GetSection("ClientHost");
                    if (setting != null) {
                        _clientHost = setting.Value;
                    }
                }

                if (string.IsNullOrWhiteSpace(_clientHost)) {
                    _clientHost = "https://s-open.qiniuniu99.com";
                }

                return _clientHost;
            }
        }

        /// <summary>
        ///     数据库的链接字符串
        /// </summary>
        public string ConnectionString {
            get {
                if (_connectionString.IsNullOrEmpty()) {
                    var setting = _systemConfiguration.GetSection("ConnectionString");
                    if (setting != null) {
                        _connectionString = setting.Value;
                    }
                }

                return _connectionString;
            }
        }

        /// <summary>
        ///     cookie认证标识
        /// </summary>
        public string AuthenticationScheme {
            get {
                var value = "Cookies";

                return value;
            }
        }

        /// <summary>
        ///     Gets or sets the mongo database connection string.
        ///     MongoDb数据库链接字符串
        /// </summary>
        public MongoDbConnection MongoDbConnection {
            get {
                if (_mongoDbConnection == null) {
                    var connections = _systemConfiguration.GetSection("ConnectionStrings");
                    var setting = connections.GetSection("MongoDbConnection");
                    if (setting != null) {
                        _mongoDbConnection = new MongoDbConnection {
                            Database = setting.GetSection("Database").Value,
                            ConnectionString = setting.GetSection("ConnectionString").Value
                        };
                    }
                }

                return _mongoDbConnection;
            }
        }

        /// <summary>
        ///     测试配置
        /// </summary>
        public OpenApiSetting OpenApiSetting {
            get {
                var setting = _systemConfiguration.GetSection("OpenApiSetting");
                if (setting != null) {
                    var testConnection = new OpenApiSetting {
                        Id = setting.GetSection("Id").Value,
                        Key = setting.GetSection("Key").Value,
                        DiyUrl = setting.GetSection("DiyUrl").Value,
                        DiyOpenUrl = setting.GetSection("DiyOpenUrl").Value,
                        Url = setting.GetSection("Url").Value
                    };
                    return testConnection;
                }

                return null;
            }
        }

        /// <summary>
        ///     测试配置
        /// </summary>
        public TestBaseConfig TestConfig {
            get {
                var setting = _systemConfiguration.GetSection("TestConfig");
                if (setting != null) {
                    var testConnection = new TestBaseConfig {
                        BaseUrl = setting.GetSection("BaseUrl").Value,
                        UserName = setting.GetSection("UserName").Value,
                        Password = setting.GetSection("Password").Value
                    };
                    return testConnection;
                }

                return null;
            }
        }

        /// <summary>
        ///     当前模板路径
        /// </summary>
        public string Theme {
            get {
                var value = "Theme";
                var setting = _systemConfiguration.GetSection("Theme");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        ///     当前手机模板路径
        /// </summary>
        public string MobileTheme {
            get {
                var value = "Theme";
                var setting = _systemConfiguration.GetSection("MobileTheme");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        ///     模板启用模式，方便调试模板用
        ///     auto :表示根据浏览器自动识别
        ///     pc:表示启用电脑版本模板
        ///     moblie:表示启用手机版本模板
        /// </summary>
        public string ThemeModel {
            get {
                var value = "auto";
                var setting = _systemConfiguration.GetSection("ThemeModel");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        /// <summary>
        ///     wwwroot根目录访问路径，默认应该为/或者/wwwroot/
        /// </summary>
        public string WebRootUrl { get; set; }

        /// <summary>
        ///     上传最大限制，单位KB
        /// </summary>
        public long UploadMaxSize {
            get {
                var setting = _systemConfiguration.GetSection("UploadMaxSize");
                if (setting != null) {
                    return setting.Value.ConvertToLong(20480000);
                }

                return 20480000;
            }
        }

        /// <summary>
        ///     非托管文件允许上传的类型
        /// </summary>
        public string UploadFiles {
            get {
                var setting = _systemConfiguration.GetSection("UploadFiles");
                if (setting != null) {
                    if (setting.Value != null) {
                        return setting.Value;
                    }
                }

                return ".jpg,.jpeg,.png,.gif,.bmp,.rar,.zip,.7z,.ico,.icon";
            }
        }

        /// <summary>
        ///     是否为开发环境
        /// </summary>
        public bool IsDevelopment {
            get {
                var setting = _systemConfiguration.GetSection("IsDevelopment");
                if (setting != null) {
                    return setting.Value.ConvertToBool(false);
                }

                return false;
            }
        }

        /// <summary>
        ///     是否为多租户模式
        /// </summary>
        public bool IsTenant {
            get {
                var setting = _systemConfiguration.GetSection("IsTenant");
                if (setting != null) {
                    return setting.Value.ConvertToBool(false);
                }

                return false;
            }
        }

        /// <summary>
        ///     主租户
        /// </summary>
        public string TenantMaster { get; set; } = "master";

        /// <summary>
        ///     diy全局css保持方式(all:所有系统存在的组件，used:只在数据库中使用的组件，默认为all)
        /// </summary>
        public string SaveStyle {
            get {
                var value = "all";
                var setting = _systemConfiguration.GetSection("SaveStyle");
                if (setting != null) {
                    if (!setting.Value.IsNullOrEmpty()) {
                        value = setting.Value;
                    }
                }

                return value;
            }
        }

        public MySqlConfig MySqlConfig => new MySqlConfig {
            ConnectionString = _systemConfiguration["ConnectionStrings:MySQL:ConnectionString"],
            ProviderName = _systemConfiguration["ConnectionStrings:MySQL:ProviderName"]
        };

        /// <summary>
        ///     从文件读取网站配置
        /// </summary>
        /// <param name="path"></param>
        internal static AppSettingConfig FromFile(string path) {
            var json = File.ReadAllText(path);
            var config = JsonConvert.DeserializeObject<AppSettingConfig>(json);
            return config;
        }

        /// <summary>
        ///     从配置中读取webSiteConfig
        /// </summary>
        /// <param name="configuration"></param>
        internal static AppSettingConfig FromConfig(IConfiguration configuration) {
            var websiteConfig = new AppSettingConfig(configuration);
            return websiteConfig;
        }
    }

    public class MySqlConfig {
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
    }

    /// <summary>
    ///     测试配置
    /// </summary>
    public class TestBaseConfig {

        /// <summary>
        ///     Gets or sets the base URL.
        /// </summary>
        public string BaseUrl { get; set; } = "http://test.5ug.com";

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        public string UserName { get; set; } = "admin";

        /// <summary>
        ///     Gets or sets the passwrod.
        /// </summary>
        public string Password { get; set; } = "123456";
    }

    /// <summary>
    ///     授权配置
    /// </summary>
    public class OpenApiSetting {

        /// <summary>
        ///     授权服务网址
        /// </summary>
        public string Url { get; set; } = "https://diyadmin.5ug.com";

        /// <summary>
        ///     前端模板地址
        /// </summary>
        public string DiyOpenUrl { get; set; } = "https://diyopen.5ug.com/";

        /// <summary>
        ///     DIY Service Url Open项目Url
        /// </summary>
        public string DiyUrl { get; set; } = "https://diyservice.5ug.com";

        public string Id { get; set; }

        public string Key { get; set; }
    }
}