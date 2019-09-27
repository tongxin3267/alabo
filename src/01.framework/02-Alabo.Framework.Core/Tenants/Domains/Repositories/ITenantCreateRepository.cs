using Alabo.Domains.Repositories;
using Alabo.Users.Entities;
using System.Collections.Generic;

namespace Alabo.Framework.Core.Tenants.Domains.Repositories
{
    /// <summary>
    ///     ITenantCreateRepository
    /// </summary>
    public interface ITenantCreateRepository : IRepository<User, long>
    {
        /// <summary>
        ///     init tenant sql
        /// </summary>
        /// <returns></returns>
        void InitTenantSql();

        /// <summary>
        ///     create database
        /// </summary>
        void CreateDatabase(string databaseName);

        /// <summary>
        ///     create database
        /// </summary>
        void DeleteDatabase(string databaseName);

        /// <summary>
        ///     is exists database
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        bool IsExistsDatabase(string databaseName);

        /// <summary>
        ///     execute sql
        /// </summary>
        /// <param name="sqlList"></param>
        void ExecuteSql(List<string> sqlList);
    }
}