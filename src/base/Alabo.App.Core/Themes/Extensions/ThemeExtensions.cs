using System;
using Alabo.Core.Enums.Enum;
using Alabo.Extensions;

namespace Alabo.App.Core.Themes.Extensions {

    /// <summary>
    ///     模板扩展
    /// </summary>
    public static class ThemeExtensions {

        /// <summary>
        ///    根据终端类型转换Url
        /// </summary>
        /// <param name="value"></param>
        /// <param name="clientType"></param>
        public static string ToClientUrl(this string value, ClientType clientType) {
            if (value.IsNullOrEmpty()) {
                return string.Empty;
            }

            // pc和H5格式处理
            if (clientType == ClientType.PcWeb) {
                value = value.Replace("/pages/user?path=", "", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("pages/user?path=", "", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("/pages/index?path=", "", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("pages/index?path=", "", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("/pages", "", StringComparison.OrdinalIgnoreCase);
            }

            if (clientType == ClientType.WeChatLite || clientType == ClientType.WapH5) {
                value = value.Replace("/share/show?id=", "/pages/index?path=share_show?id=", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("/articles/topline/show?id=", "/pages/index?path=articles_topline_show&id=", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("/share/show?id=", "/pages/index?path=share_show?id=", StringComparison.OrdinalIgnoreCase);
                value = value.Replace("\"/index\"", "\"/pages/index\"");
                value = value.Replace("\"/index", "\"/pages/index");
            }

            return value;
        }

        /// <summary>
        ///     URL安全处理，确保与系统URL对应
        ///     规则：全部小写，以/开头，结尾去掉/
        ///     示范：/user/reg
        /// </summary>
        /// <param name="url"></param>
        public static string ToSafeUrl(this string url) {
            if (url.IsNullOrEmpty()) {
                return string.Empty;
            }

            url = url.ToLower()
                .Replace("/pages", string.Empty, StringComparison.OrdinalIgnoreCase)
                //.Replace("/page", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("pages/", string.Empty, StringComparison.OrdinalIgnoreCase)
                // .Replace("page/", string.Empty, StringComparison.OrdinalIgnoreCase)
                .Replace("///", "/").Replace("////", "/").Replace("//", "/");
            if (url.Substring(0, 1) != "/") {
                url = "/" + url;
            }

            if (url.Substring(url.Length - 1, 1) == "/") {
                url = url.Substring(0, url.Length - 1);
            }

            url = url.Replace("//", "/").Replace("///", "/").Replace("////", "/");
            return url.Replace("//", "/");
        }

        /// <summary>
        ///     Api网址安全处理，确保与系统URL对应
        ///     规则：全部小写，以/开头，结尾去掉/
        ///     示范：/Api/user/reg
        /// </summary>
        /// <param name="apiUrl"></param>
        public static string ToSafeApiUrl(this string apiUrl) {
            if (apiUrl.IsNullOrEmpty()) {
                return string.Empty;
            }

            apiUrl = apiUrl.ToSafeUrl();
            if (apiUrl.Substring(0, 1) != "/") {
                apiUrl = "/" + apiUrl;
            }

            if (apiUrl.Substring(apiUrl.Length - 1, 1) == "/") {
                apiUrl = apiUrl.Substring(0, apiUrl.Length - 1);
            }

            return apiUrl.Trim().ToLower();
        }

        /// <summary>
        ///     Api网址安全处理，确保与系统URL对应
        /// </summary>
        /// <param name="componentPath"></param>
        public static string ToSafeComponentPath(this string componentPath) {
            if (componentPath.IsNullOrEmpty()) {
                return string.Empty;
            }

            componentPath = componentPath.ToSafeUrl();
            if (componentPath.Substring(0, 1) != "/") {
                componentPath = "/" + componentPath;
            }

            if (componentPath.Substring(componentPath.Length - 1, 1) == "/") {
                componentPath = componentPath.Substring(0, componentPath.Length - 1);
            }

            return componentPath;
        }

        /// <summary>
        ///     根据路径获取变量名称
        /// </summary>
        /// <param name="path"></param>
        public static string ToVariableName(this string path) {
            var nameList = path.ToSafeUrl().Replace("/", "").Replace("zk-", ".").Replace("-", ".").ToSplitList(".");
            var name = string.Empty;
            foreach (var item in nameList) {
                name += item.ToFristUpper();
            }

            return name;
        }

        /// <summary>
        /// 将Url转换成路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlToPath(string url) {
            if (url.IsNullOrEmpty()) {
                return string.Empty;
            }

            url = url.ToSafeUrl();
            if (url.Substring(0, 1) == "/") {
                url = url.Substring(1, url.Length - 2);
                url = url.Replace("/", "_");
                url = "/pages/index?path=" + url;
            }
            return url;
        }

        public static string UrlToVariableName(this string url) {
            var nameList = url.ToSafeUrl().Replace("/", ".").ToSplitList(".");
            var name = string.Empty;
            foreach (var item in nameList) {
                name += item.ToFristUpper();
            }

            return name;
        }
    }
}