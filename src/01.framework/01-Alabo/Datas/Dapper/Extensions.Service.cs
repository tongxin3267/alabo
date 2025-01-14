﻿using Alabo.Datas.Dapper.Configs;
using Alabo.Datas.Dapper.Handlers;
using Alabo.Datas.Dapper.MySql;
using Alabo.Datas.Dapper.PgSql;
using Alabo.Datas.Dapper.SqlServer;
using Alabo.Datas.Enums;
using Alabo.Datas.Matedatas;
using Alabo.Datas.Sql;
using Alabo.Datas.Sql.Queries;
using Alabo.Datas.Sql.Queries.Builders.Abstractions;
using Alabo.Extensions;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Alabo.Datas.Dapper {

    /// <summary>
    ///     服务扩展
    /// </summary>
    public static class Extensions {

        /// <summary>
        ///     注册Sql查询服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="action">Sql查询配置</param>
        public static IServiceCollection AddSqlQuery(this IServiceCollection services,
            Action<SqlQueryConfig> action = null) {
            return AddSqlQuery(services, action, null, null);
        }

        /// <summary>
        ///     注册Sql查询服务
        /// </summary>
        /// <typeparam name="TDatabase">IDatabase实现类型，提供数据库连接</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="action">Sql查询配置</param>
        public static IServiceCollection AddSqlQuery<TDatabase>(this IServiceCollection services,
            Action<SqlQueryConfig> action = null)
            where TDatabase : class, IDatabase {
            return AddSqlQuery(services, action, typeof(TDatabase), null);
        }

        /// <summary>
        ///     注册Sql查询服务
        /// </summary>
        /// <typeparam name="TDatabase">IDatabase实现类型，提供数据库连接</typeparam>
        /// <typeparam name="TEntityMatedata">IEntityMatedata实现类型,提供实体元数据解析</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="action">Sql查询配置</param>
        public static IServiceCollection AddSqlQuery<TDatabase, TEntityMatedata>(this IServiceCollection services,
            Action<SqlQueryConfig> action = null)
            where TDatabase : class, IDatabase
            where TEntityMatedata : class, IEntityMatedata {
            return AddSqlQuery(services, action, typeof(TDatabase), typeof(TEntityMatedata));
        }

        /// <summary>
        ///     注册Sql查询服务
        /// </summary>
        private static IServiceCollection AddSqlQuery(IServiceCollection services, Action<SqlQueryConfig> action,
            Type database, Type entityMatedata) {
            if (database != null) {
                services.TryAddScoped(database);
                services.TryAddScoped(typeof(IDatabase), t => t.GetService(database));
            }

            services.TryAddScoped<ISqlQuery, SqlQuery>();
            if (entityMatedata != null) {
                services.TryAddScoped(typeof(IEntityMatedata), t => t.GetService(entityMatedata));
            }

            var config = new SqlQueryConfig();
            action?.Invoke(config);
            AddSqlBuilder(services, config);
            RegisterTypeHandlers();
            return services;
        }

        /// <summary>
        ///     配置Sql生成器
        /// </summary>
        private static void AddSqlBuilder(IServiceCollection services, SqlQueryConfig config) {
            switch (config.DatabaseType) {
                case DatabaseType.SqlServer:
                    services.TryAddScoped<ISqlBuilder, SqlServerBuilder>();
                    return;

                case DatabaseType.PgSql:
                    services.TryAddScoped<ISqlBuilder, PgSqlBuilder>();
                    return;

                case DatabaseType.MySql:
                    services.TryAddScoped<ISqlBuilder, MySqlBuilder>();
                    return;

                default:
                    throw new NotImplementedException($"Sql生成器未实现 {config.DatabaseType.Description()} 数据库");
            }
        }

        /// <summary>
        ///     注册类型处理器
        /// </summary>
        private static void RegisterTypeHandlers() {
            SqlMapper.AddTypeHandler(typeof(string), new StringTypeHandler());
        }
    }
}