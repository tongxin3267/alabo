using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;

namespace Alabo.Core.Helpers
{
    public static class HttpContextHelper
    {
        /// <summary>
        ///     获取Cookie值
        /// </summary>
        public static string GetCookie(this HttpContext context, string plugin, string name)
        {
            // HttpContext.Current等于null时使用备用Cookies储存
            var key = $"{plugin}.{name}";
            if (context == null) {
                return null;
            }

            var cookie = context.Request.Cookies ?? new RequestCookieCollection();
            if (!cookie.ContainsKey(key)) {
                return null;
            }

            var x = cookie[key];
            if (string.IsNullOrEmpty(x)) {
                return null;
            }

            cookie.TryGetValue(key, out var value);
            return WebUtility.UrlDecode(value);
            ;
        }

        /// <summary>
        ///     获取Cookie值
        /// </summary>
        public static string GetCookie(this HttpContext context, string key)
        {
            if (context == null) {
                return null;
            }

            var cookie = context.Request.Cookies ?? new RequestCookieCollection();
            if (!cookie.ContainsKey(key)) {
                return null;
            }

            var x = cookie[key];
            if (string.IsNullOrEmpty(x)) {
                return null;
            }

            cookie.TryGetValue(key, out var value);
            return WebUtility.UrlDecode(value);
            ;
        }

        /// <summary>
        ///     设置Cookie值
        /// </summary>
        public static bool PutCookie(this HttpContext context, string key, string value,
            DateTime? expired = default, bool httpOnly = false)
        {
            if (context == null) {
                return false;
            }

            var cookie = context.Response.Cookies;
            var option = new CookieOptions
            {
                Expires = expired.HasValue ? expired.Value : DateTime.MinValue,
                HttpOnly = httpOnly
            };
            try
            {
                context.Response.Cookies.Delete(key);
                cookie.Append(key, WebUtility.UrlEncode(value), option);
                return true;
            }
            catch
            {
                return false; // 连接中断时这里会抛例外
            }
        }

        /// <summary>
        ///     设置Cookie值
        /// </summary>
        public static bool PutCookie(this HttpContext context, string plugin, string name, string value,
            DateTime? expired = default, bool httpOnly = false)
        {
            var key = $"{plugin}.{name}";
            if (context == null) {
                return false;
            }

            var cookie = context.Response.Cookies;
            var option = new CookieOptions
            {
                Expires = expired.HasValue ? expired.Value : DateTime.MinValue,
                HttpOnly = httpOnly
            };
            try
            {
                context.Response.Cookies.Delete(key);
                cookie.Append(key, WebUtility.UrlEncode(value), option);
                return true;
            }
            catch
            {
                return false; // 连接中断时这里会抛例外
            }
        }

        /// <summary>
        ///     以GET方式异步发起请求
        /// </summary>
        /// <param name="url">请求URL字符串</param>
        public static async Task<string> RequestOfGet(string url)
        {
            var client = new HttpClient();
            var result = await client.GetStringAsync(url);
            return result;
        }

        /// <summary>
        ///     以POST方式异步发起请求
        /// </summary>
        /// <param name="url">请求URL字符串</param>
        /// <param name="data">请求的HttpContent对象</param>
        public static async Task<string> RequestOfPost(string url, HttpContent data)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(url, data);
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static string GetSession(this HttpContext context, string key)
        {
            return context.Session.GetString(key);
        }

        public static void RemoveSession(this HttpContext context, string key)
        {
            context.Session.Remove(key);
        }

        public static void SetSession(this HttpContext context, string key, string value)
        {
            context.Session.SetString(key, value);
        }

        public static string GetIpAddress(this HttpContext context)
        {
            var ip = string.Empty;
            var remoteIpAddress = context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress;
            if (remoteIpAddress != null) {
                ip = remoteIpAddress.ToString();
            }

            return ip;
        }
    }
}