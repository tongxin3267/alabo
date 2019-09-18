using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Alabo.Core.Localize;
using ZKCloud.Open.ApiBase.Services;

namespace Alabo.App.Core.Themes.Extensions {

    public static class MvcExtensions {

        public static IHtmlContent AuthenticationStatus(this IHtmlHelper html) {
            var services = html.ViewContext.HttpContext.RequestServices;
            var serverAuthenticationManager = services.GetService<IServerAuthenticationManager>();
            if (serverAuthenticationManager.Token == null) {
                return new HtmlString(
                    $"<a   href=\"/Admin/Authentication\"  class='btn btn-danger btn-circle  btn-sm authentication authentication-status'>{html.T("未激活")}</a>");
            }

            if (serverAuthenticationManager.IsAuthenticated) {
                return new HtmlString(
                    $"<a href=\"/Admin/Message/Authentication\"  class='btn btn-success btn-circle  btn-sm authentication authentication-status' title=\" {html.T("激活时间:")}{serverAuthenticationManager.Token.AuthenticationTime:yyyy-MM-dd} {html.T("过期时间:")}{serverAuthenticationManager.Token.ExpiresTime:yyyy-MM-dd}\">{html.T("已激活")}</a>");
            }

            var message = serverAuthenticationManager.GetLastMessage(out _);
            return new HtmlString(
                $"<a  href=\"/Admin/Authentication\" class='btn btn-danger btn-circle  btn-sm authentication authentication-status' title=\"{html.T("错误信息:")}{html.Encode(message)}\">{html.T("激活失败")}</a>");
        }
    }
}