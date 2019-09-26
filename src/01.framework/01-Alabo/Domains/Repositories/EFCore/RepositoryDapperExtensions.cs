using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Dapper;

namespace Alabo.Domains.Repositories.EFCore
{
    /// <summary>
    ///     通过Dapper获取数据
    ///     https://github.com/StackExchange/Dapper
    ///     教程：https://www.cnblogs.com/lunawzh/p/6607116.html
    /// </summary>
    public static class RepositoryDapperExtensions
    {
        private static DbConnection Connection(this IRepositoryContext context)
        {
            var con = context;
            //context.DbContexts.TryGetValue(RuntimeContext.CurrentTenant,
            //    out EfCoreRepositoryContext.InnerDbContext _context);
            //if (_context is DbContext) {
            //    return (_context as DbContext).Database.GetDbConnection();
            //}

            throw new System.Exception("no DbConnection supported for this context.");
        }

        public static T DapperGet<T>(this IRepositoryContext context, string sql, object param = null)
            where T : class
        {
            var connection = context.Connection();
            if (connection.State != ConnectionState.Open) connection.Open();

            var result = connection.QueryFirstOrDefault<T>(sql, param);
            return result;
        }

        public static IEnumerable<T> DapperGetList<T>(this IRepositoryContext context, string sql, object param = null)
            where T : class
        {
            var connection = context.Connection();
            if (connection.State != ConnectionState.Open) connection.Open();

            var result = connection.Query<T>(sql, param);
            return result;
        }

        public static IEnumerable<TReturn> DapperGetList<TFirst, TSecond, TReturn>(this IRepositoryContext context,
            string sql, Func<TFirst, TSecond, TReturn> map)
        {
            var connection = context.Connection();
            if (connection.State != ConnectionState.Open) connection.Open();

            var result = connection.Query(sql, map);
            return result;
        }

        public static TReturn DapperGet<TFirst, TSecond, TReturn>(this IRepositoryContext context, string sql,
            Func<TFirst, TSecond, TReturn> map)
        {
            var connection = context.Connection();
            if (connection.State != ConnectionState.Open) connection.Open();

            var result = connection.Query(sql, map);
            return result.FirstOrDefault();
        }
    }
}