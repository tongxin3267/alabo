using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Alabo.Domains.Repositories.EFCore.Context;
using Alabo.Domains.Repositories.Model;

namespace Alabo.Domains.Repositories.EFCore
{
    /// <summary>
    ///     Class RepositoryContextExtensions.
    /// </summary>
    public static class RepositoryContextExtensions
    {
        /// <summary>
        ///     执行Sql语句
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        public static int ExecuteNonQuery(this IRepositoryContext context, string sql)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        ///     Determines whether [is has data] [the specified SQL].
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        public static bool IsHasData(this IRepositoryContext context, string sql)
        {
            var result = false;
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            var reader = command.ExecuteReader();
            result = reader.HasRows;
            reader.Dispose();

            return result;
        }

        /// <summary>
        ///     执行Sql语句列表
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sqlList">sql语句列表</param>
        public static int ExecuteNonQuery(this IRepositoryContext context, List<string> sqlList)
        {
            var count = 0;
            foreach (var item in sqlList)
            {
                ExecuteNonQuery(context, item);
                count++;
            }

            return count;
        }

        /// <summary>
        ///     Begins the native database transaction.
        /// </summary>
        /// <param name="context">上下文</param>
        public static DbTransaction BeginNativeDbTransaction(this IRepositoryContext context)
        {
            var connection = context.Connection();
            if (connection.State != ConnectionState.Open) {
                connection.Open();
            }

            return connection.BeginTransaction();
        }

        /// <summary>
        ///     Executes the non query.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static int ExecuteNonQuery(this IRepositoryContext context, string sql, params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        ///     执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sqlStringList">多条SQL语句</param>
        public static int ExecuteSqlList(this IRepositoryContext context, List<string> sqlStringList)
        {
            var command = context.Connection().CreateCommand();
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            DbTransaction transaction = null;
            var count = 0;
            try
            {
                transaction = context.Connection().BeginTransaction();
                command.Transaction = transaction;

                foreach (var item in sqlStringList)
                {
                    command.CommandText = item;
                    count += command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
                transaction.Rollback();
                return 0;
            }
            finally
            {
                if (transaction != null) {
                    transaction.Dispose();
                }

                command.Dispose();
            }

            return count;
        }

        /// <summary>
        ///     Executes the batch. 批量执行sql语句 带参数的sql语句，参考会员注册的用法
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="batchParameters">The batch parameters.</param>
        public static bool ExecuteBatch(this IRepositoryContext context, IEnumerable<BatchParameter> batchParameters)
        {
            var sqlList = batchParameters.Select(r => r.Sql);
            var paramList = batchParameters.Select(r => r.Parameters);
            return ExecuteBatch(context, sqlList, paramList);
        }

        /// <summary>
        ///     批量执行sql语句
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sqlList">sql语句集合</param>
        /// <param name="paramList">参数数组集合</param>
        public static bool ExecuteBatch(this IRepositoryContext context, IEnumerable<string> sqlList,
            IEnumerable<DbParameter[]> paramList)
        {
            var command = context.Connection().CreateCommand();
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            DbTransaction trans = null;
            try
            {
                trans = context.Connection().BeginTransaction();
                command.Transaction = trans;
                var length = sqlList.Count();
                IEnumerable<DbParameter> parameters = null;
                for (var i = 0; i < length; i++)
                {
                    command.CommandText = sqlList.ElementAt(i);
                    command.Parameters.Clear();
                    parameters = paramList.ElementAt(i);
                    foreach (var pm in parameters) {
                        command.Parameters.Add(pm);
                    }

                    command.ExecuteNonQuery();
                }

                trans.Commit();
                return true;
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
                if (trans != null) {
                    trans.Rollback();
                }

                throw;
            }
            finally
            {
                if (trans != null) {
                    trans.Dispose();
                }

                command.Dispose();
            }
        }

        /// <summary>
        ///     Executes the scalar.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        public static object ExecuteScalar(this IRepositoryContext context, string sql)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }

        /// <summary>
        ///     Executes the scalar.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static object ExecuteScalar(this IRepositoryContext context, string sql, params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        public static DbDataReader ExecuteDataReader(this IRepositoryContext context, string sql)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static DbDataReader ExecuteDataReader(this IRepositoryContext context, string sql,
            params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            if (context.IsInTransaction) {
                command.Transaction = context.GetDbTransaction();
            }

            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        /// <summary>
        ///     Executes the non query.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static int ExecuteNonQuery(this IRepositoryContext context, DbTransaction transaction, string sql,
            params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteNonQuery();
        }

        /// <summary>
        ///     Executes the scalar.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="sql">The SQL.</param>
        public static object ExecuteScalar(this IRepositoryContext context, DbTransaction transaction, string sql)
        {
            var command = context.Connection().CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }

        /// <summary>
        ///     Executes the scalar.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static object ExecuteScalar(this IRepositoryContext context, DbTransaction transaction, string sql,
            params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteScalar();
        }

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="sql">The SQL.</param>
        public static DbDataReader ExecuteDataReader(this IRepositoryContext context, DbTransaction transaction,
            string sql)
        {
            var command = context.Connection().CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        /// <summary>
        ///     Executes the data reader.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="transaction">The transaction.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameters">The parameters.</param>
        public static DbDataReader ExecuteDataReader(this IRepositoryContext context, DbTransaction transaction,
            string sql, params DbParameter[] parameters)
        {
            var command = context.Connection().CreateCommand();
            command.Transaction = transaction;
            command.CommandText = sql;
            if (parameters != null && parameters.Length > 0) {
                command.Parameters.AddRange(parameters);
            }

            if (command.Connection.State == ConnectionState.Closed) {
                command.Connection.Open();
            }

            return command.ExecuteReader();
        }

        /// <summary>
        ///     Creates the parameter.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public static DbParameter CreateParameter(this IRepositoryContext context, string name, object value)
        {
            var command = context.Connection().CreateCommand();
            var result = command.CreateParameter();
            result.ParameterName = name;
            result.Value = value;
            return result;
        }

        /// <summary>
        ///     Creates the parameter.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The 类型.</param>
        /// <param name="value">The value.</param>
        public static DbParameter CreateParameter(this IRepositoryContext context, string name, DbType type,
            object value)
        {
            var command = context.Connection().CreateCommand();
            var result = command.CreateParameter();
            result.ParameterName = name;
            result.DbType = type;
            result.Value = value;
            return result;
        }

        /// <summary>
        ///     Creates the parameter.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The 类型.</param>
        /// <param name="size">The size.</param>
        /// <param name="value">The value.</param>
        public static DbParameter CreateParameter(this IRepositoryContext context, string name, DbType type, int size,
            object value)
        {
            var command = context.Connection().CreateCommand();
            var result = command.CreateParameter();
            result.ParameterName = name;
            result.DbType = type;
            result.Size = size;
            result.Value = value;
            return result;
        }

        /// <summary>
        ///     Connections the specified context.
        /// </summary>
        /// <param name="context">上下文</param>
        private static DbConnection Connection(this IRepositoryContext context)
        {
            var dbConnection = context.UnitOfWork.GetConnection();
            return (DbConnection) dbConnection;
        }

        /// <summary>
        ///     创建数据库
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="tableName">Name of the table.</param>
        public static bool CreateDataTable(this IRepositoryContext context, string tableName)
        {
            try
            {
                var createDbStr = $"Create database {tableName}";
                var command = context.Connection().CreateCommand();
                command.ExecuteNonQuery();
                return false;
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return true;
            }
        }

        /// <summary>
        ///     Query IEnumerable<T> by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(this IRepositoryContext context, string sql, params object[] args)
            where T : new()
        {
            using (var dr = context.ExecuteDataReader(sql))
            {
                if (dr.HasRows)
                {
                    var propCache = new Dictionary<string, PropertyInfo>();
                    var drFields = new List<string>();
                    for (var i = 0; i < dr.FieldCount; i++) {
                        drFields.Add(dr.GetName(i));
                    }

                    while (dr.Read())
                    {
                        var insT = new T();
                        if (propCache.Count == drFields.Count)
                        {
                            var propList = insT.GetType().GetProperties();
                            foreach (var field in drFields)
                            {
                                var prop = propCache[field];
                                var value = dr[field];
                                if (value != DBNull.Value) {
                                    prop.SetValue(insT, value, null);
                                }
                            }
                        }
                        else
                        {
                            var propList = insT.GetType().GetProperties();
                            foreach (var prop in propList)
                            {
                                var field = prop.Name;
                                if (!drFields.Contains(field)) {
                                    continue;
                                }

                                if (!prop.CanWrite) {
                                    continue;
                                }

                                var value = dr[field];
                                if (value != DBNull.Value) {
                                    prop.SetValue(insT, value, null);
                                }

                                if (!propCache.ContainsKey(field)) {
                                    propCache.Add(field, prop);
                                }
                            }

                            // Re-balance PropCache & drFields
                            if (propCache.Count != drFields.Count)
                            {
                                var interset = drFields.Intersect(propCache.Select(x => x.Key)).ToList();
                                drFields = interset;

                                var subSet = propCache.Select(x => x.Key).Except(interset);
                                foreach (var item in subSet) {
                                    propCache.Remove(item);
                                }
                            }
                        }

                        yield return insT;
                    }
                }
            }
        }

        /// <summary>
        ///     Fetch List<T> by sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="sql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static List<T> Fetch<T>(this IRepositoryContext context, string sql, params object[] args)
            where T : new()
        {
            return context.Query<T>(sql, args).ToList();
        }
    }
}