using Alabo.Datas.Dapper;
using Alabo.Datas.Ef.Configs;
using Alabo.Datas.Ef.MySql;
using Alabo.Datas.Ef.PgSql;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Datas.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Alabo.Datas.Ef
{
    /// <summary>
    ///     服务扩展
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        ///     注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        public static IServiceCollection AddService<TService, TImplementation>(this IServiceCollection services)
            where TService : class, IService
            where TImplementation : ServiceBase, TService
        {
            services.TryAddScoped<TService>(t => t.GetService<TImplementation>());
            return services;
        }

        /// <summary>
        ///     注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configAction">配置操作</param>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> configAction)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            services.AddDbContext<TImplementation>(configAction);
            services.TryAddScoped<TService>(t => t.GetService<TImplementation>());
            services.AddSqlQuery<TImplementation, TImplementation>(config =>
                config.DatabaseType = GetDbType<TImplementation>());
            return services;
        }

        /// <summary>
        ///     获取数据库类型
        /// </summary>
        private static DatabaseType GetDbType<TUnitOfWork>()
        {
            var type = typeof(TUnitOfWork).BaseType;
            if (type == typeof(MsSqlUnitOfWork)) return DatabaseType.SqlServer;

            if (type == typeof(MySqlUnitOfWork)) return DatabaseType.MySql;

            if (type == typeof(PgSqlUnitOfWork)) return DatabaseType.PgSql;

            return DatabaseType.SqlServer;
        }

        /// <summary>
        ///     注册工作单元服务
        /// </summary>
        /// <typeparam name="TService">工作单元接口类型</typeparam>
        /// <typeparam name="TImplementation">工作单元实现类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="connection">连接字符串</param>
        /// <param name="level">Ef日志级别</param>
        public static IServiceCollection AddUnitOfWork<TService, TImplementation>(this IServiceCollection services,
            string connection, EfLogLevel level = EfLogLevel.Sql)
            where TService : class, IUnitOfWork
            where TImplementation : UnitOfWorkBase, TService
        {
            EfConfig.LogLevel = level;
            return AddUnitOfWork<TService, TImplementation>(services,
                builder => { ConfigConnection<TImplementation>(builder, connection); });
        }

        /// <summary>
        ///     配置连接字符串
        /// </summary>
        private static void ConfigConnection<TImplementation>(DbContextOptionsBuilder builder, string connection)
            where TImplementation : UnitOfWorkBase
        {
            switch (GetDbType<TImplementation>())
            {
                case DatabaseType.SqlServer:
                    builder.UseSqlServer(connection);
                    return;

                case DatabaseType.MySql:
                    builder.UseMySql(connection);
                    return;

                case DatabaseType.PgSql:
                    builder.UseNpgsql(connection);
                    return;
            }
        }
    }
}