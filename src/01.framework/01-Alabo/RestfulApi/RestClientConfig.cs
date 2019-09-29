using Alabo.Extensions;
using Alabo.Runtime;
using Alabo.Security;
using Alabo.Security.Sessions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alabo.RestfulApi
{
    public sealed class RestClientConfig
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _systemConfiguration;

        private string _openApiId;

        private string _openApiKey;

        public RestClientConfig(IHttpContextAccessor httpContextAccessor, IConfiguration systemConfiguration)
        {
            _httpContextAccessor = httpContextAccessor;
            _systemConfiguration = systemConfiguration;
        }

        public string OpenApiId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_openApiId))
                {
                    var openApiSetting = _systemConfiguration.GetSection("OpenApiSetting");
                    if (openApiSetting != null) _openApiId = openApiSetting.GetSection("Id")?.Value;
                }

                return _openApiId;
            }
        }

        /// <summary>
        ///     Gets the open API key.
        /// </summary>
        /// <value>The open API key.</value>
        public string OpenApiKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_openApiKey))
                {
                    var openApiSetting = _systemConfiguration.GetSection("OpenApiSetting");
                    if (openApiSetting != null) _openApiKey = openApiSetting.GetSection("Key")?.Value;
                }

                return _openApiKey;
            }
        }

        /// <summary>
        ///     设置多租户
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public async Task SetTenant(string tenant)
        {
            if (tenant.IsNullOrEmpty()) throw new ArgumentNullException(nameof(tenant));

            var session = new Session();
            var basicUser = new BasicUser
            {
                Tenant = tenant
            };
            var claimsPrincipal = session.CreateClaimsIdentity(basicUser);
            await _httpContextAccessor.HttpContext.SignInAsync(
                RuntimeContext.Current.WebsiteConfig.AuthenticationScheme,
                new ClaimsPrincipal(claimsPrincipal));
        }
    }
}