using System;
using System.Linq;
using System.Threading.Tasks;
using Alabo.Extensions;
using Microsoft.AspNetCore.Http;

namespace Alabo.Tenants
{
    /// <summary>
    ///     tenant middleware
    /// </summary>
    public class TenantMiddleware
    {
        /// <summary>
        ///     next
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        ///     constructor
        /// </summary>
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        ///     invoke
        /// </summary>
        /// <param name="context">Http上下文</param>
        public async Task Invoke(HttpContext context)
        {
            if (TenantContext.IsTenant)
            {
                var tenantHeader = context.Request.Headers["zk-tenant"];
                var tenant = tenantHeader.FirstOrDefault();
                if (tenant.IsNullOrEmpty() || tenant.Contains("null", StringComparison.OrdinalIgnoreCase))
                    tenant = TenantContext.Master;

                TenantContext.CurrentTenant = tenant.Trim().ToLower();
            }

            await _next(context);
        }
    }
}