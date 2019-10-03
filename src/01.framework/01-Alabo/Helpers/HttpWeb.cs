using Alabo.Extensions;
using Alabo.Runtime;
using Alabo.Security;
using Alabo.Security.Enums;
using Alabo.Security.Principals;
using Alabo.Tenants;
using Alabo.Tenants.Domain.Entities;
using Alabo.Tenants.Domain.Services;
using Alabo.Web.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Web;
using HttpRequest = Microsoft.AspNetCore.Http.HttpRequest;
using WebClient = Alabo.Web.Clients.WebClient;

namespace Alabo.Helpers
{
    /// <summary>
    ///     Web操作
    /// </summary>
    public static class HttpWeb
    {
        #region 静态构造方法

        /// <summary>
        ///     初始化Web操作
        /// </summary>
        static HttpWeb()
        {
            HttpContextAccessor = Ioc.Resolve<IHttpContextAccessor>();
            Environment = Ioc.Resolve<IHostingEnvironment>();
        }

        #endregion 静态构造方法

        #region 是否为租户模式

        /// <summary>
        ///     是否为租户模式
        /// </summary>
        public static bool IsTenant
        {
            get
            {
                var result = RuntimeContext.Current.WebsiteConfig.IsTenant;
                return result;
            }
        }

        #endregion 是否为租户模式

        #region 当前登录用户的租户

        /// <summary>
        ///     当前登录用户的租户
        /// </summary>
        public static string Tenant
        {
            get
            {
                var result = TenantContext.CurrentTenant;
                if (result.IsNullOrEmpty()) {
                    result = "master";
                }

                return result;
            }
        }

        #endregion 当前登录用户的租户

        #region 获取当前访问的站点或租户的站点

        /// <summary>
        ///     获取当前访问的站点，或租户的站点
        /// </summary>
        public static TenantSite Site => Ioc.Resolve<ITenantService>().Site();

        #endregion 获取当前访问的站点或租户的站点

        #region Identity(当前用户身份)

        /// <summary>
        ///     当前用户身份
        /// </summary>
        public static ClaimsIdentity Identity
        {
            get
            {
                if (Claims.Identity is ClaimsIdentity identity) {
                    return identity;
                }

                return UnauthenticatedIdentity.Instance;
            }
        }

        #endregion Identity(当前用户身份)

        #region Body(请求正文)

        /// <summary>
        ///     请求正文
        /// </summary>
        public static string Body
        {
            get
            {
                using (var reader = new StreamReader(Request.Body))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        #endregion Body(请求正文)

        #region Browser(浏览器)

        /// <summary>
        ///     浏览器
        /// </summary>
        public static string Browser => HttpContext?.Request?.Headers["User-Agent"];

        #endregion Browser(浏览器)

        #region RootPath(根路径)

        /// <summary>
        ///     根路径
        /// </summary>
        public static string RootPath => Environment?.ContentRootPath;

        #endregion RootPath(根路径)

        #region WebRootPath(Web根路径)

        /// <summary>
        ///     Web根路径，即wwwroot
        /// </summary>
        public static string WebRootPath => Environment?.WebRootPath;

        #endregion WebRootPath(Web根路径)

        #region 从当前Http请求中，获取表单中获取值

        /// <summary>
        ///     从当前Http请求中，获取表单中获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        public static T GetValue<T>(string key)
        {
            var value = HttpContext.Request.Form[key];
            if (value.IsNullOrEmpty()) {
                return Convert.To<T>(null);
            }

            var result = Convert.To<T>(value);
            return result;
        }

        #endregion 从当前Http请求中，获取表单中获取值

        #region GetFiles(获取客户端文件集合)

        /// <summary>
        ///     获取客户端文件集合
        /// </summary>
        public static List<IFormFile> GetFiles()
        {
            var result = new List<IFormFile>();
            var files = HttpContext.Request.Form.Files;
            if (files == null || files.Count == 0) {
                return result;
            }

            result.AddRange(files.Where(file => file?.Length > 0));
            return result;
        }

        #endregion GetFiles(获取客户端文件集合)

        #region GetFile(获取客户端文件)

        /// <summary>
        ///     获取客户端文件
        /// </summary>
        public static IFormFile GetFile()
        {
            var files = GetFiles();
            return files.Count == 0 ? null : files[0];
        }

        #endregion GetFile(获取客户端文件)

        #region Url(请求地址)

        /// <summary>
        ///     请求地址
        /// </summary>
        public static string Url => HttpContext?.Request?.GetDisplayUrl();

        /// <summary>
        ///     客户端请求地址主机 带http或者https， 主要指的是zkweb项目
        ///     格式：http://localhost:2000
        ///     https://www.5ug.com
        /// </summary>
        public static string ClientHost
        {
            get
            {
                var host = ((HttpRequestHeaders)HttpContext.Request.Headers).HeaderOrigin;
                var clientHost = string.Empty;
                if (!host.IsNullOrEmpty()) {
                    clientHost = host[0].ToStr();
                }

                return clientHost;
            }
        }

        /// <summary>
        ///     服务器请求地址主机 带http或者https， 主要指的是ZKCloud项目
        ///     格式：http://localhost:9018
        ///     https://diyservice.5ug.com
        /// </summary>
        public static string ServiceHost
        {
            get
            {
                var host =
                    ((HttpRequestHeaders)((HttpProtocol)((DefaultHttpContext)HttpContext).Features).RequestHeaders)
                    .HeaderHost;

                var features = HttpContext.Features;
                //var scheme = ((Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http.HttpProtocol)features).Scheme;
                var serviceHost = "http://" + host; // scheme+"://" + host;//改成http
                return serviceHost;
            }
        }

        /// <summary>
        ///     服务器请求地址主机 带http或者https， 主要指的是ZKCloud项目
        ///     格式：http://localhost:9018
        ///     https://diyservice.5ug.com
        /// </summary>
        public static string ServiceHostUrl
        {
            get
            {
                var host =
                    ((HttpRequestHeaders)((HttpProtocol)((DefaultHttpContext)HttpContext).Features).RequestHeaders)
                    .HeaderHost;

                var features = HttpContext.Features;
                var scheme = ((HttpProtocol)features).Scheme;
                var serviceHost = scheme + "://" + host; //改成http
                return serviceHost;
            }
        }

        #endregion Url(请求地址)

        #region User(当前用户安全主体)

        /// <summary>
        ///     当前登录用户
        /// </summary>
        public static BasicUser User
        {
            get
            {
                if ((UserId == 0) | UserName.IsNullOrEmpty()) {
                    return null;
                }

                var user = new BasicUser
                {
                    Id = UserId,
                    UserName = UserName
                };
                user.Email = Identity.GetValue(ClaimTypes.Email);
                user.Name = Identity.GetValue(ClaimTypes.Name);

                var authType = Identity.GetValue(ClaimTypes.AuthorizationDecision);
                if (!Enum.TryParse(authType, out LoginAuthorizeType authorizeType))
                {
                    authorizeType = LoginAuthorizeType.Login;
                    user.LoginAuthorizeType = authorizeType;
                }

                return user;
            }
        }

        /// <summary>
        ///     当前登录用户Id
        /// </summary>
        public static long UserId
        {
            get
            {
                var result = Identity.GetValue(ClaimTypes.NameIdentifier);
                return result.ConvertToLong();
            }
        }

        /// <summary>
        ///     用户名
        /// </summary>
        public static string UserName
        {
            get
            {
                var result = Identity.GetValue(ClaimTypes.Name);
                return result;
            }
        }

        /// <summary>
        ///     当前用户安全主体
        /// </summary>
        public static ClaimsPrincipal Claims
        {
            get
            {
                if (HttpContext == null) {
                    return UnauthenticatedPrincipal.Instance;
                }

                if (HttpContext.User is ClaimsPrincipal principal) {
                    return principal;
                }

                return UnauthenticatedPrincipal.Instance;
            }
        }

        #endregion User(当前用户安全主体)

        #region 属性

        /// <summary>
        ///     Http上下文访问器
        /// </summary>
        public static IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        ///     当前Http上下文
        /// </summary>
        public static HttpContext HttpContext => HttpContextAccessor?.HttpContext;

        /// <summary>
        ///     当前Http请求
        /// </summary>
        public static HttpRequest Request => HttpContext?.Request;

        /// <summary>
        ///     当前Http响应
        /// </summary>
        public static HttpResponse Response => HttpContext?.Response;

        /// <summary>
        ///     宿主环境
        /// </summary>
        public static IHostingEnvironment Environment { get; set; }

        #endregion 属性

        #region Client( Web客户端 )

        /// <summary>
        ///     Web客户端，用于发送Http请求
        /// </summary>
        public static WebClient Client()
        {
            return new WebClient();
        }

        /// <summary>
        ///     Web客户端，用于发送Http请求
        /// </summary>
        /// <typeparam name="TResult">返回结果类型</typeparam>
        public static WebClient<TResult> Client<TResult>() where TResult : class
        {
            return new WebClient<TResult>();
        }

        #endregion Client( Web客户端 )

        #region Ip(客户端Ip地址)

        /// <summary>
        ///     Ip地址
        /// </summary>
        private static string _ip;

        /// <summary>
        ///     设置Ip地址
        /// </summary>
        /// <param name="ip">Ip地址</param>
        public static void SetIp(string ip)
        {
            _ip = ip;
        }

        /// <summary>
        ///     重置Ip地址
        /// </summary>
        public static void ResetIp()
        {
            _ip = null;
        }

        /// <summary>
        ///     客户端Ip地址
        /// </summary>
        public static string Ip
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ip) == false) {
                    return _ip;
                }

                var list = new[] { "127.0.0.1", "::1" };
                var result = (HttpContext?.Connection?.RemoteIpAddress).SafeString();
                if (string.IsNullOrWhiteSpace(result) || list.Contains(result)) {
                    result = GetLanIp();
                }

                return result;
            }
        }

        /// <summary>
        ///     获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName())) {
                if (hostAddress.AddressFamily == AddressFamily.InterNetwork) {
                    return hostAddress.ToString();
                }
            }

            return string.Empty;
        }

        #endregion Ip(客户端Ip地址)

        #region Host(主机)

        /// <summary>
        ///     主机
        /// </summary>
        public static string Host => HttpContext == null ? Dns.GetHostName() : GetClientHostName();

        /// <summary>
        ///     域名和端口号
        /// </summary>
        public static string Domain =>
            ((DefaultHttpRequest)
                ((DefaultHttpContext)HttpContext).Request).Host.ToString();

        /// <summary>
        ///     请求路径
        /// </summary>
        public static string Path =>
            ((DefaultHttpRequest)
                ((DefaultHttpContext)HttpContext).Request).Path;

        /// <summary>
        ///     结构，比如http或https
        /// </summary>
        public static string Scheme => ((DefaultHttpRequest)Request).Scheme;

        /// <summary>
        ///     全域名比如：http://www.5ug.com/ http://localhost:9012
        /// </summary>
        public static string FullDomain => $"{Scheme}://{Domain}";

        /// <summary>
        ///     获取Web客户端主机名
        /// </summary>
        private static string GetClientHostName()
        {
            var address = GetRemoteAddress();
            if (string.IsNullOrWhiteSpace(address)) {
                return Dns.GetHostName();
            }

            var result = Dns.GetHostEntry(IPAddress.Parse(address)).HostName;
            if (result == "localhost.localdomain") {
                result = Dns.GetHostName();
            }

            return result;
        }

        /// <summary>
        ///     获取远程地址
        /// </summary>
        private static string GetRemoteAddress()
        {
            return HttpContext?.Request?.Headers["HTTP_X_FORWARDED_FOR"] ??
                   HttpContext?.Request?.Headers["REMOTE_ADDR"];
        }

        #endregion Host(主机)

        #region UrlEncode(Url编码)

        /// <summary>
        ///     Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, bool isUpper = false)
        {
            return UrlEncode(url, Encoding.UTF8, isUpper);
        }

        /// <summary>
        ///     Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, string encoding, bool isUpper = false)
        {
            encoding = string.IsNullOrWhiteSpace(encoding) ? "UTF-8" : encoding;
            return UrlEncode(url, Encoding.GetEncoding(encoding), isUpper);
        }

        /// <summary>
        ///     Url编码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        /// <param name="isUpper">编码字符是否转成大写,范例,"http://"转成"http%3A%2F%2F"</param>
        public static string UrlEncode(string url, Encoding encoding, bool isUpper = false)
        {
            var result = HttpUtility.UrlEncode(url, encoding);
            if (isUpper == false) {
                return result;
            }

            return GetUpperEncode(result);
        }

        /// <summary>
        ///     获取大写编码字符串
        /// </summary>
        private static string GetUpperEncode(string encode)
        {
            var result = new StringBuilder();
            var index = int.MinValue;
            for (var i = 0; i < encode.Length; i++)
            {
                var character = encode[i].ToString();
                if (character == "%") {
                    index = i;
                }

                if (i - index == 1 || i - index == 2) {
                    character = character.ToUpper();
                }

                result.Append(character);
            }

            return result.ToString();
        }

        #endregion UrlEncode(Url编码)

        #region UrlDecode(Url解码)

        /// <summary>
        ///     Url解码
        /// </summary>
        /// <param name="url">url</param>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

        /// <summary>
        ///     Url解码
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="encoding">字符编码</param>
        public static string UrlDecode(string url, Encoding encoding)
        {
            return HttpUtility.UrlDecode(url, encoding);
        }

        #endregion UrlDecode(Url解码)
    }
}