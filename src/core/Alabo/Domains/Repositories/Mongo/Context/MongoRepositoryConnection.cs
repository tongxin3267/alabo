using System;
using System.Collections.Concurrent;
using MongoDB.Driver;
using Alabo.Runtime;
using Alabo.Tenants;

namespace Alabo.Domains.Repositories.Mongo.Context
{
    /*
     IMongoDatabase IMongoClient等都没有Close方法，而且没有继承IDispose接口，
     这意味着你不需要显示的关闭数据库操作的连接。这个和以前ado的连接使用方式不同。
     可以直接使用单例模式，不必频繁的去实例化该对象，也不用代码显式的去打开和关闭数据库连接，
     驱动内部已经帮我们做好了连接池管理。
     */

    /// <summary>
    ///     Class MongoDbContext.
    /// </summary>
    public static class MongoRepositoryConnection
    {
        /// <summary>
        ///     mongo database cache
        /// </summary>
        private static readonly ConcurrentDictionary<string, IMongoDatabase> _mongoDatabase =
            new ConcurrentDictionary<string, IMongoDatabase>();

        /// <summary>
        ///     The client settings
        /// </summary>
        private static readonly MongoClientSettings ClientSettings;

        /// <summary>
        ///     The mongo URL
        /// </summary>
        private static readonly MongoUrl MongoUrl;

        /// <summary>
        ///     The client
        /// </summary>
        private static readonly MongoClient Client;

        /// <summary>
        ///     Initializes static members of the <see cref="MongoDbContext" /> class.
        /// </summary>
        static MongoRepositoryConnection()
        {
            ConnectString = RuntimeContext.Current.WebsiteConfig.MongoDbConnection.ConnectionString;
            MongoUrl = new MongoUrl(ConnectString);
            /*
             mongo实例其实已经是一个现成的连接池了，而且线程安全。
             这个内置的连接池默认初始了10个连接，每一个操作（增删改查等）都会获取一个连接，执行操作后释放连接。
              */
            ClientSettings = MongoClientSettings.FromUrl(MongoUrl);
            ClientSettings.ConnectionMode = ConnectionMode.Direct;
            ClientSettings.ConnectTimeout = new TimeSpan(0, 0, 0, 30, 0); //30秒超时
            ClientSettings.MinConnectionPoolSize = 8; //当链接空闲时,空闲线程池中最大链接数，默认0
            ClientSettings.MaxConnectionPoolSize = 300; //默认100
            ClientSettings.WriteConcern = WriteConcern.Acknowledged;
            Client = new MongoClient(ClientSettings);
        }

        /// <summary>
        ///     Gets the database.
        /// </summary>
        public static IMongoDatabase Database => GetDatabase();

        /*兼容Legacy模式的数据库*/

        /// <summary>
        ///     Gets the legacy database.
        /// </summary>
        public static MongoDatabase LegacyDatabase => GetLegacyDatabase();

        /// <summary>
        ///     Gets or sets the connect string.
        /// </summary>
        private static string ConnectString { get; }

        /// <summary>
        ///     Gets or sets the name of the data base.
        /// </summary>
        private static string DataBaseName
        {
            get
            {
                var database = RuntimeContext.GetTenantDataBase();
                if (!TenantContext.IsTenant) {
                    return database;
                }

                var tenantName = TenantContext.CurrentTenant;
                if (string.IsNullOrWhiteSpace(tenantName) || tenantName.ToLower() == TenantContext.Master.ToLower()) {
                    return database;
                }

                return RuntimeContext.GetTenantDataBase(tenantName);
            }
        }

        /// <summary>
        ///     清空表
        /// </summary>
        /// <param name="tableName"></param>
        public static void DropTable(string tableName)
        {
            Database.DropCollection(tableName);
        }

        /// <summary>
        ///     get the database.
        /// </summary>
        private static IMongoDatabase GetDatabase()
        {
            var dataBaseName = DataBaseName;
            //exists database return
            if (_mongoDatabase.ContainsKey(dataBaseName))
            {
                return _mongoDatabase[dataBaseName];
            }

            //not exists add adn return
            var database = Client.GetDatabase(dataBaseName);
            _mongoDatabase.TryAdd(dataBaseName, database);
            return database;
        }

        /// <summary>
        ///     获取s the legacy database.
        /// </summary>
        internal static MongoDatabase GetLegacyDatabase()
        {
#pragma warning disable 618
            var server = Client.GetServer();
#pragma warning restore 618
            return server.GetDatabase(DataBaseName);
        }
    }
}