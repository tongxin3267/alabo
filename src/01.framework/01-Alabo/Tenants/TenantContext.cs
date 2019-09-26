using System;
using System.Collections.Concurrent;
using Alabo.Datas.UnitOfWorks;
using Alabo.Dependency;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Tenants.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Tenants
{
    public class TenantContext
    {
        /// <summary>
        ///     Tenants
        /// </summary>
        private static readonly ConcurrentDictionary<string, string> _tenantDictionary =
            new ConcurrentDictionary<string, string>();

        /// <summary>
        ///     current tenant thread static
        /// </summary>
        [ThreadStatic] public static string CurrentTenant;

        /// <summary>
        ///     is tenant
        /// </summary>
        public static bool IsTenant => HttpWeb.IsTenant;

        /// <summary>
        ///     master tenant
        /// </summary>
        public static string Master => "master";

        /// <summary>
        ///     当前租户的站点Id
        /// </summary>
        public static ObjectId SiteId { get; set; }

        /// <summary>
        ///     get current tenant
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentTenant()
        {
            if (string.IsNullOrWhiteSpace(CurrentTenant)) return string.Empty;

            return GetTenant(CurrentTenant);
        }

        /// <summary>
        ///     get master tenant
        /// </summary>
        /// <returns></returns>
        public static string GetMasterTenant()
        {
            return GetTenant(Master);
        }

        /// <summary>
        ///     get tenant
        /// </summary>
        /// <param name="name"></param>
        public static string GetTenant(string name)
        {
            if (_tenantDictionary.ContainsKey(name)) return _tenantDictionary[name];

            return string.Empty;
        }

        /// <summary>
        ///     add master tenant
        /// </summary>
        /// <param name="value"></param>
        public static void AddMasterTenant(string value)
        {
            AddTenant(Master, value);
        }

        /// <summary>
        ///     add tenant
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void AddTenant(string name, string value)
        {
            if (!_tenantDictionary.ContainsKey(name)) _tenantDictionary.TryAdd(name, value);
        }

        /// <summary>
        ///     get master tenant
        /// </summary>
        /// <returns></returns>
        public static Tenant GetDefaultMasterTenant()
        {
            return new Tenant
            {
                Name = Master,
                DatabaseName = RuntimeContext.GetTenantDataBase()
            };
        }

        /// <summary>
        ///     Switch database
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="tenantName"></param>
        public static void SwitchDatabase(IScope scope, string tenantName)
        {
            if (string.IsNullOrWhiteSpace(tenantName)) return;

            CurrentTenant = tenantName;
            Ioc.CurrentScope = scope.GetHashCode();
            var unitOfWork = (UnitOfWorkBase) scope.Resolve<IUnitOfWork>();
            unitOfWork.SwitchTenantDatabase();
        }
    }
}