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
        public static string GetConnectionStringForMaster(this string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString)) return string.Empty;

            var database = RuntimeContext.GetTenantDataBase();
            return connectionString.Replace(
                "Initial Catalog=" + RuntimeContext.Current.WebsiteConfig.MongoDbConnection.Database,
                "Initial Catalog=" + database);
        }

        /// <summary>
        ///     Get tenant connection string
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string GetConnectionStringForTenant(this string connectionString, string tenantName)
        {
            if (string.IsNullOrWhiteSpace(connectionString) || string.IsNullOrWhiteSpace(tenantName))
                return string.Empty;

            var database = RuntimeContext.GetTenantDataBase();
            var databaseName = RuntimeContext.GetTenantDataBase(tenantName);
            return connectionString.Replace(database, databaseName);
        }

        /// <summary>
        ///     Switch tenant database.
        /// </summary>
        public static string GetConnectionStringForTenant(this string connectionString)
        {
            if (!TenantContext.IsTenant) return connectionString;

            var tenantName = TenantContext.CurrentTenant;
            if (string.IsNullOrWhiteSpace(tenantName)) return connectionString;
            //Current tenant is main and connection string is not equal switch to main database
            if (tenantName.ToLower() == TenantContext.Master.ToLower()) return connectionString;

            return GetConnectionStringForTenant(connectionString, TenantContext.GetCurrentTenant());
        }
    }
}