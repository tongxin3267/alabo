using System;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Security.Claims;

namespace Alabo.Security.Sessions {

    /// <summary>
    ///     用户会话
    /// </summary>
    public class Session : ISession {

        /// <summary>
        ///     空用户会话
        /// </summary>
        public static readonly ISession Null = NullSession.Instance;

        /// <summary>
        ///     用户会话
        /// </summary>
        public static readonly ISession Instance = new Session();

        /// <summary>
        ///     是否为管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     是否认证
        /// </summary>
        public bool IsAuthenticated => HttpWeb.Identity.IsAuthenticated;

        /// <summary>
        ///     用户标识
        /// </summary>
        public string UserId {
            get {
                var result = HttpWeb.Identity.GetValue(JwtClaimTypes.Subject);
                return string.IsNullOrWhiteSpace(result)
                    ? HttpWeb.Identity.GetValue(ClaimTypes.NameIdentifier)
                    : result;
            }
        }

        public string Tenant => HttpWeb.Tenant;

        /// <summary>
        ///     登录多租户模式
        /// </summary>
        /// <param name="tenant"></param>
        public async Task SignInTenant(string tenant) {
            if (HttpWeb.IsTenant) {
                if (tenant.IsNullOrEmpty()) {
                    throw new ValidException("多租户模式，请设置租户");
                }

                var basicUser = HttpWeb.User;
                if (basicUser == null) {
                    basicUser = new BasicUser();
                    basicUser.Tenant = tenant.ToLower().Trim();
                }

                var claimsPrincipal = CreateClaimsIdentity(basicUser);
                var httpContextAccessor = Ioc.Resolve<IHttpContextAccessor>();
                await httpContextAccessor.HttpContext.SignInAsync(
                    RuntimeContext.Current.WebsiteConfig.AuthenticationScheme,
                    new ClaimsPrincipal(claimsPrincipal));
                httpContextAccessor = Ioc.Resolve<IHttpContextAccessor>();
            }
        }

        /// <summary>
        ///     创建登录标识
        /// </summary>
        public ClaimsIdentity CreateClaimsIdentity(BasicUser user) {
            var authenticationScheme = RuntimeContext.Current.WebsiteConfig.AuthenticationScheme;

            var identity = new ClaimsIdentity(authenticationScheme);
            identity.AddClaim(new Claim(
                "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "ASP.NET Identity"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
            if (!string.IsNullOrWhiteSpace(user.Tenant)) {
                identity.AddClaim(new Claim(IdentityClaimTypes.TenantName, user.Tenant));
            }

            if (!string.IsNullOrWhiteSpace(user.UserName)) {
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            }

            if (!string.IsNullOrWhiteSpace(user.Email)) {
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            }

            if (!string.IsNullOrWhiteSpace(user.Name)) {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));
            }

            identity.AddClaim(new Claim(ClaimTypes.AuthorizationDecision, user.LoginAuthorizeType.ToString()));
            return identity;
        }

        /// <summary>
        ///     用户登录
        /// </summary>
        /// <param name="user"></param>
        public async Task SignInUser(BasicUser user) {
            if (string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Email)) {
                throw new ArgumentException("UserName and email is requred in user model.");
            }

            if (user.Id == 0) {
                throw new ArgumentException("userId is requred in user model.");
            }

            var claimsPrincipal = CreateClaimsIdentity(user);
            var httpContextAccessor = Ioc.Resolve<IHttpContextAccessor>();
            await httpContextAccessor.HttpContext.SignInAsync(RuntimeContext.Current.WebsiteConfig.AuthenticationScheme,
                new ClaimsPrincipal(claimsPrincipal));
        }
    }
}