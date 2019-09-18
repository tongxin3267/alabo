using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Security.Claims;
using Alabo.Security.Sessions;
using Convert = Alabo.Helpers.Convert;

namespace Alabo.Security
{
    /// <summary>
    ///     用户会话扩展
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        ///     获取当前操作人标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetUserId(this ISession session)
        {
            return session.UserId.ToGuid();
        }

        /// <summary>
        ///     获取当前操作人标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetUserId<T>(this ISession session)
        {
            return Convert.To<T>(session.UserId);
        }

        /// <summary>
        ///     获取当前操作人用户名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetUserName(this ISession session)
        {
            var result = HttpWeb.Identity.GetValue(JwtClaimTypes.Name);
            return string.IsNullOrWhiteSpace(result) ? HttpWeb.Identity.GetValue(ClaimTypes.Name) : result;
        }

        /// <summary>
        ///     获取当前操作人姓名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetFullName(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.FullName);
        }

        /// <summary>
        ///     获取当前操作人电子邮件
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetEmail(this ISession session)
        {
            var result = HttpWeb.Identity.GetValue(JwtClaimTypes.Email);
            return string.IsNullOrWhiteSpace(result) ? HttpWeb.Identity.GetValue(ClaimTypes.Email) : result;
        }

        /// <summary>
        ///     获取当前操作人手机号
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetMobile(this ISession session)
        {
            var result = HttpWeb.Identity.GetValue(JwtClaimTypes.PhoneNumber);
            return string.IsNullOrWhiteSpace(result) ? HttpWeb.Identity.GetValue(ClaimTypes.MobilePhone) : result;
        }

        /// <summary>
        ///     获取当前应用程序标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetApplicationId(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.ApplicationId).ToGuid();
        }

        /// <summary>
        ///     获取当前应用程序标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetApplicationId<T>(this ISession session)
        {
            return Convert.To<T>(HttpWeb.Identity.GetValue(IdentityClaimTypes.ApplicationId));
        }

        /// <summary>
        ///     获取当前应用程序编码
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetApplicationCode(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.ApplicationCode);
        }

        /// <summary>
        ///     获取当前应用程序名称
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetApplicationName(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.ApplicationName);
        }

        /// <summary>
        ///     获取当前租户标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static Guid GetTenantId(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.TenantId).ToGuid();
        }

        /// <summary>
        ///     获取当前租户标识
        /// </summary>
        /// <param name="session">用户会话</param>
        public static T GetTenantId<T>(this ISession session)
        {
            return Convert.To<T>(HttpWeb.Identity.GetValue(IdentityClaimTypes.TenantId));
        }

        /// <summary>
        ///     获取当前租户编码
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetTenantCode(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.TenantCode);
        }

        /// <summary>
        ///     获取当前租户名称
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetTenantName(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.TenantName);
        }

        /// <summary>
        ///     获取当前操作人角色标识列表
        /// </summary>
        /// <param name="session">用户会话</param>
        public static List<Guid> GetRoleIds(this ISession session)
        {
            return session.GetRoleIds<Guid>();
        }

        /// <summary>
        ///     获取当前操作人角色标识列表
        /// </summary>
        /// <param name="session">用户会话</param>
        public static List<T> GetRoleIds<T>(this ISession session)
        {
            return Convert.ToList<T>(HttpWeb.Identity.GetValue(IdentityClaimTypes.RoleIds));
        }

        /// <summary>
        ///     获取当前操作人角色名
        /// </summary>
        /// <param name="session">用户会话</param>
        public static string GetRoleName(this ISession session)
        {
            return HttpWeb.Identity.GetValue(IdentityClaimTypes.RoleName);
        }
    }
}