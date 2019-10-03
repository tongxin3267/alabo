using System.Data.SqlClient;
using Alabo.Runtime;

namespace Alabo.Tenants.Extensions
{
    public static class ConnectionStringExtension
    {
        /// <summary>
        ///     Get master connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetConnectionStringForMaster(this string connectionString) {
            if (string.IsNullOrWhiteSpace(connectionString)) {
                return string.Empty;
            }

            var database = RuntimeContext.GetTenantSqlDataBase();
            connectionString = connectionString.Replace(
               "Initial Catalog=" + RuntimeContext.Current.WebsiteConfig.MsSqlDbConnection.Database,
               "Initial Catalog=" + database);
            return connectionString;
        }

        /// <summary>
        ///     Get tenant connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="tenantName"></param>
        /// <returns></returns>
        public static string GetConnectionStringForTenant(this string connectionString, string tenantName) {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(tenantName)) {
                return string.Empty;
            }

            var database = RuntimeContext.GetTenantMongodbDataBase();
            var databaseName = RuntimeContext.GetTenantMongodbDataBase(tenantName);
            return connectionString.Replace(database, databaseName);
        }
    }
}