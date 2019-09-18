using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.Core.Webs
{
    /// <summary>
    ///     Web操作
    /// </summary>
    public class Webs
    {
        #region 静态构造方法

        /// <summary>
        ///     初始化Web操作
        /// </summary>
        public Webs(HttpContext httpContext)
        {
            try
            {
                HttpContext = httpContext;
                Environment = Ioc.Resolve<IHostingEnvironment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion 静态构造方法

        #region Browser(浏览器)

        /// <summary>
        ///     浏览器
        /// </summary>
        public string Browser => HttpContext?.Request?.Headers["User-Agent"];

        #endregion Browser(浏览器)

        #region RootPath(根路径)

        /// <summary>
        ///     根路径
        /// </summary>
        public string RootPath => Environment?.ContentRootPath;

        #endregion RootPath(根路径)

        #region WebRootPath(Web根路径)

        /// <summary>
        ///     Web根路径，即wwwroot
        /// </summary>
        public string WebRootPath => Environment?.WebRootPath;

        #endregion WebRootPath(Web根路径)

        #region GetFiles(获取客户端文件集合)

        /// <summary>
        ///     获取客户端文件集合
        /// </summary>
        public List<IFormFile> GetFiles()
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
        public IFormFile GetFile()
        {
            var files = GetFiles();
            return files.Count == 0 ? null : files[0];
        }

        #endregion GetFile(获取客户端文件)

        #region Url(请求地址)

        /// <summary>
        ///     请求地址
        /// </summary>
        public string Url => HttpContext?.Request?.GetDisplayUrl();

        /// <summary>
        ///     请求地址
        /// </summary>
        public string BaseUrl => HttpContext?.Request?.Scheme + "://" + HttpContext?.Request?.Host.Value;

        #endregion Url(请求地址)

        #region 属性

        /// <summary>
        ///     当前Http上下文
        /// </summary>
        public HttpContext HttpContext { get; set; }

        /// <summary>
        ///     宿主环境
        /// </summary>
        public IHostingEnvironment Environment { get; set; }

        #endregion 属性

        #region Ip(客户端Ip地址)

        /// <summary>
        ///     客户端Ip地址
        /// </summary>
        public string Ip
        {
            get
            {
                var list = new[] {"127.0.0.1", "::1"};
                var result = HttpContext?.Connection?.RemoteIpAddress.ToStr();
                if (string.IsNullOrWhiteSpace(result) || list.Contains(result)) {
                    result = GetLanIp();
                }

                return result;
            }
        }

        /// <summary>
        ///     获取局域网IP
        /// </summary>
        private string GetLanIp()
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
        public string Host => HttpContext == null ? Dns.GetHostName() : GetClientHostName();

        /// <summary>
        ///     获取Web客户端主机名
        /// </summary>
        private string GetClientHostName()
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
        private string GetRemoteAddress()
        {
            return HttpContext?.Request?.Headers["HTTP_X_FORWARDED_FOR"] ??
                   HttpContext?.Request?.Headers["REMOTE_ADDR"];
        }

        #endregion Host(主机)
    }
}