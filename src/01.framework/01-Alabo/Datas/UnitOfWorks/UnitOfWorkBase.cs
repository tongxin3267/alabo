using Alabo.Datas.Ef.Configs;
using Alabo.Datas.Ef.Logs;
using Alabo.Datas.Ef.Map;
using Alabo.Datas.Matedatas;
using Alabo.Datas.Sql;
using Alabo.Domains.Auditing;
using Alabo.Exceptions;
using Alabo.Logging;
using Alabo.Reflections;
using Alabo.Security.Sessions;
using Alabo.Tenants;
using Alabo.Tenants.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Alabo.Datas.UnitOfWorks {

    /// <summary>
    ///     工作单元
    /// </summary>
    public abstract class UnitOfWorkBase : DbContext, IUnitOfWork, IDatabase, IEntityMatedata {

        #region Commit(提交)

        /// <summary>
        ///     提交,返回影响的行数
        /// </summary>
        public int Commit() {
            try {
                return SaveChanges();
            } catch (DbUpdateConcurrencyException ex) {
                throw new ConcurrencyException(ex);
            }
        }

        #endregion Commit(提交)

        #region CommitAsync(异步提交)

        /// <summary>
        ///     异步提交,返回影响的行数
        /// </summary>
        public async Task<int> CommitAsync() {
            try {
                return await SaveChangesAsync();
            } catch (DbUpdateConcurrencyException ex) {
                throw new ConcurrencyException(ex);
            }
        }

        #endregion CommitAsync(异步提交)

        #region SaveChangesAsync(异步保存更改)

        /// <summary>
        ///     异步保存更改
        /// </summary>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            SaveChangesBefore();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion SaveChangesAsync(异步保存更改)

        #region 构造方法

        /// <summary>
        ///     初始化Entity Framework工作单元
        /// </summary>
        /// <param name="options">配置</param>
        /// <param name="manager">工作单元服务</param>
        protected UnitOfWorkBase(DbContextOptions options, IUnitOfWorkManager manager)
            : base(options) {
            manager?.Register(this);
            TraceId = Guid.NewGuid();
            Session = Security.Sessions.Session.Instance;
            DbContextOptions = options;
            SwitchTenantDatabase();
        }

        /// <summary>
        ///     Switch tenant database.
        /// </summary>
        public void SwitchTenantDatabase() {
            //run in thread 'dbConnection.ConnectionString' is no pwd
            var dbConnection = GetConnection();
            var mainConnectionString = TenantContext.GetMasterTenant();
            if (string.IsNullOrWhiteSpace(mainConnectionString)) {
                mainConnectionString = string.IsNullOrWhiteSpace(ConnectionString)
                    ? dbConnection.ConnectionString
                    : ConnectionString;
                TenantContext.AddMasterTenant(mainConnectionString);
            }

            if (string.IsNullOrWhiteSpace(ConnectionString)) {
                ConnectionString = mainConnectionString;
            }

            if (!TenantContext.IsTenant) {
                return;
            }

            var tenantName = TenantContext.CurrentTenant;
            if (string.IsNullOrWhiteSpace(tenantName)) {
                return;
            }
            //Current tenant is main and connection string is not equal switch to main database
            if (tenantName.ToLower() == TenantContext.Master.ToLower()) {
                if (mainConnectionString != dbConnection.ConnectionString) {
                    SwitchDatabase(dbConnection, mainConnectionString);
                }

                return;
            }

            //get tenant connection string
            var newConnectionString = TenantContext.GetCurrentTenant();
            if (string.IsNullOrWhiteSpace(newConnectionString)) {
                newConnectionString = mainConnectionString.GetConnectionStringForTenant(tenantName);
                TenantContext.AddTenant(tenantName, newConnectionString);
            }

            SwitchDatabase(dbConnection, newConnectionString);
        }

        /// <summary>
        ///     switch database
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="connectionString"></param>
        private void SwitchDatabase(IDbConnection dbConnection, string connectionString) {
            //check state
            if (dbConnection.State != ConnectionState.Closed) {
                //run in thread connection must dispose.
                dbConnection.Close();
                dbConnection.Dispose();
            }

            //switch database.
            GetConnection().ConnectionString = connectionString;
            ConnectionString = connectionString;
        }

        public DbContextOptions DbContextOptions { get; set; }

        #endregion 构造方法

        #region GetConnection(获取数据库连接)

        /// <summary>
        ///     获取数据库连接
        /// </summary>
        public IDbConnection GetConnection() {
            return Database.GetDbConnection();
        }

        /// <summary>
        ///     数据库连接字符串
        /// </summary>
        public string ConnectionString { get; private set; }

        #endregion GetConnection(获取数据库连接)

        #region 属性

        /// <summary>
        ///     跟踪号
        /// </summary>
        public Guid TraceId { get; set; }

        /// <summary>
        ///     用户会话
        /// </summary>
        public ISession Session { get; set; }

        #endregion 属性

        #region OnConfiguring(配置)

        /// <summary>
        ///     配置
        /// </summary>
        /// <param name="builder">配置生成器</param>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            EnableLog(builder);
        }

        /// <summary>
        ///     启用日志
        /// </summary>
        protected void EnableLog(DbContextOptionsBuilder builder) {
            var log = GetLog();
            if (IsEnabled(log) == false) {
                return;
            }

            builder.EnableSensitiveDataLogging();
            builder.UseLoggerFactory(new LoggerFactory(new[] { GetLogProvider(log) }));
        }

        /// <summary>
        ///     获取日志操作
        /// </summary>
        protected virtual ILog GetLog() {
            try {
                return Log.GetLog(EfLog.TraceLogName);
            } catch {
                return Log.Null;
            }
        }

        /// <summary>
        ///     是否启用Ef日志
        /// </summary>
        private bool IsEnabled(ILog log) {
            return EfConfig.LogLevel != EfLogLevel.Off && log.IsTraceEnabled;
        }

        /// <summary>
        ///     获取日志提供器
        /// </summary>
        protected virtual ILoggerProvider GetLogProvider(ILog log) {
            return new EfLogProvider(log, this);
        }

        #endregion OnConfiguring(配置)

        #region OnModelCreating(配置映射)

        /// <summary>
        ///     配置映射
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            foreach (var mapper in GetMaps()) {
                mapper.Map(modelBuilder);
            }
        }

        /// <summary>
        ///     获取映射配置列表
        /// </summary>
        private IEnumerable<IMap> GetMaps() {
            var result = GetMapTypes();
            return result;
        }

        /// <summary>
        ///     获取映射类型列表
        /// </summary>
        protected virtual IEnumerable<IMap> GetMapTypes() {
            return Reflection.GetInstancesByInterface<IMap>();
        }

        #endregion OnModelCreating(配置映射)

        #region SaveChanges(保存更改)

        /// <summary>
        ///     保存更改
        /// </summary>
        public override int SaveChanges() {
            SaveChangesBefore();
            return base.SaveChanges();
        }

        /// <summary>
        ///     保存更改前操作
        /// </summary>An error occurred while updating the entries
        protected virtual void SaveChangesBefore() {
            foreach (var entry in ChangeTracker.Entries()) {
                switch (entry.State) {
                    case EntityState.Added:
                        InterceptAddedOperation(entry);
                        break;

                    case EntityState.Modified:
                        InterceptModifiedOperation(entry);
                        break;

                    case EntityState.Deleted:
                        InterceptDeletedOperation(entry);
                        break;
                }
            }
        }

        /// <summary>
        ///     拦截添加操作
        /// </summary>
        protected virtual void InterceptAddedOperation(EntityEntry entry) {
            InitCreationAudited(entry);
            InitModificationAudited(entry);
        }

        /// <summary>
        ///     初始化创建审计信息
        /// </summary>
        private void InitCreationAudited(EntityEntry entry) {
            CreationAuditedInitializer.Init(entry.Entity, GetSession());
        }

        /// <summary>
        ///     获取用户会话
        /// </summary>
        protected virtual ISession GetSession() {
            return Session;
        }

        /// <summary>
        ///     初始化修改审计信息
        /// </summary>
        private void InitModificationAudited(EntityEntry entry) {
            ModificationAuditedInitializer.Init(entry.Entity, GetSession());
        }

        /// <summary>
        ///     拦截修改操作
        /// </summary>
        protected virtual void InterceptModifiedOperation(EntityEntry entry) {
            InitModificationAudited(entry);
        }

        /// <summary>
        ///     拦截删除操作
        /// </summary>
        protected virtual void InterceptDeletedOperation(EntityEntry entry) {
        }

        #endregion SaveChanges(保存更改)

        #region 获取元数据

        /// <summary>
        ///     获取表名
        /// </summary>
        /// <param name="entity">实体类型</param>
        public string GetTable(Type entity) {
            if (entity == null) {
                return null;
            }

            var entityType = Model.FindEntityType(entity);
            return Extensions.Extensions.SafeString(entityType?.FindAnnotation("Relational:TableName")?.Value);
        }

        /// <summary>
        ///     获取架构
        /// </summary>
        /// <param name="entity">实体类型</param>
        public string GetSchema(Type entity) {
            if (entity == null) {
                return null;
            }

            var entityType = Model.FindEntityType(entity);
            return Extensions.Extensions.SafeString(entityType?.FindAnnotation("Relational:Schema")?.Value);
        }

        /// <summary>
        ///     获取列名
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <param name="property">属性名</param>
        public string GetColumn(Type entity, string property) {
            if (entity == null || string.IsNullOrWhiteSpace(property)) {
                return null;
            }

            var entityType = Model.FindEntityType(entity);
            var result = Extensions.Extensions.SafeString(entityType?.GetProperty(property)
                ?.FindAnnotation("Relational:ColumnName")?.Value);
            return string.IsNullOrWhiteSpace(result) ? property : result;
        }

        #endregion 获取元数据
    }
}