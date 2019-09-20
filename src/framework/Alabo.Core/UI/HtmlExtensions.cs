using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Alabo.Cache;
using Alabo.Core.Files;
using Alabo.Core.UI.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Web.Mvc.Attributes;
using File = System.IO.File;

namespace Alabo.Core.UI
{
    /// <summary>
    ///     HTMl扩展
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        ///     Redirects to error.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="message">The message.</param>
        /// <returns>ActionResult.</returns>
        public static void RedirectToError(this IHtmlHelper html, object message)
        {
            html.ViewContext.HttpContext.Response.Redirect($"/505?{message}");
        }

        /// <summary>
        ///     将错误信息输出到页面上
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="message">The message.</param>
        /// <returns>IHtmlContent.</returns>
        public static IHtmlContent ErrorMessage(this IHtmlHelper html, object message)
        {
            var content = $"<div class='alert alert-danger' role='alert'>{message}</div>";
            return html.Raw(content);
        }

        /// <summary>
        ///     查询参数
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public static object Query(this IHtmlHelper html, string key)
        {
            string value = html.ViewContext.HttpContext.Request.Query[key];
            if (value == null) {
                return string.Empty;
            }

            return value;
        }

        /// <summary>
        ///     图片地址处理
        /// </summary>
        /// <param name="html"></param>
        /// <param name="smallUrl"></param>
        public static string ImageUrl(this IHtmlHelper html, string smallUrl)
        {
            if (smallUrl != null)
            {
                if (File.Exists(FileHelper.RootPath + smallUrl)) {
                    return smallUrl;
                }
            }
            else
            {
                return "/wwwroot/static/images/nopic.png";
            }

            return "/wwwroot/static/images/nopic.png";
        }

        /// <summary>
        ///     设置HTML内容的显示样式
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="content">输入内容</param>
        /// <param name="style">显示格式</param>
        /// <param name="color">显示颜色</param>
        /// <returns>IHtmlContent.</returns>
        public static IHtmlContent Style(this IHtmlHelper html, object content, LabelStyle style = LabelStyle.Label,
            LabelColor color = LabelColor.Success)
        {
            var cssName = string.Empty;
            if (style == LabelStyle.Label) {
                cssName = $@"m-badge  m-badge--wide {color.GetCustomAttr<LabelCssClassAttribute>()?.CssClass}";
            }

            if (style == LabelStyle.Badges) {
                cssName = $@"m-badge  m-badge--wide {color.GetCustomAttr<LabelCssClassAttribute>()?.CssClass}";
            }

            if (style == LabelStyle.RoundlessBadges) {
                cssName =
                    $@"m-badge  m-badge--wide {
                            color.GetCustomAttr<LabelCssClassAttribute>()?.CssClass
                        } badge-roundless";
            }

            content = $@"<span class='{cssName}'>{content}</span>";
            return html.Raw(content);
        }

        /// <summary>
        ///     Tabs的图标
        ///     http://ui.5ug.com/metronic_v5.0.6.1/metronic_v5.0.6.1/theme/default/dist/default/components/icons/flaticon.html
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="i">The i.</param>
        /// <returns>System.String.</returns>
        public static string Icon(this IHtmlHelper html, int i = 0)
        {
            if (i == 0) {
                return "<i class=flaticon-multimedia ></i>";
            }

            if (i == 1) {
                return "<i class=	flaticon-graphic-2 ></i>";
            }

            if (i == 2) {
                return "<i class=flaticon-business ></i>";
            }

            if (i == 3) {
                return "<i class=flaticon-squares-4 ></i>";
            }

            if (i == 4) {
                return "<i class=flaticon-tool ></i>";
            }

            return "<i class=flaticon-diagram ></i>";
        }

        /// <summary>
        ///     获取当前页面控制器名称
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string PageController(this IHtmlHelper html)
        {
            return html.ViewContext.RouteData.Values["controller"].ToString();
        }

        /// <summary>
        ///     获取当前页面控制器名称
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string PageAction(this IHtmlHelper html)
        {
            return html.ViewContext.RouteData.Values["action"].ToString();
        }

        /// <summary>
        ///     获取当前页面的访问视图
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string PageViewName(this IHtmlHelper html)
        {
            return html.ViewContext.ExecutingFilePath;
        }

        /// <summary>
        ///     获取当前页面控制器名称
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string PageGroupName(this IHtmlHelper html)
        {
            var request = html.ViewContext.HttpContext.Request;

            return html.ViewContext.RouteData.Values["group"].ToString();
        }

        /// <summary>
        ///     获取当前页面控制器名称
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <returns>System.String.</returns>
        public static string PageAppName(this IHtmlHelper html)
        {
            return html.ViewContext.RouteData.Values["app"].ToString();
        }

        /// <summary>
        ///     显示widget路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> Widget(this IHtmlHelper html, string partialViewName)
        {
            partialViewName = $@"~/Widgets/{partialViewName}/Widget.cshtml";
            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     显示Widget的路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="groupName">分组名称</param>
        /// <param name="appName">APP应用名称</param>
        /// <param name="partialViewName">视图名称</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> Widget(this IHtmlHelper html, string groupName, string appName,
            string partialViewName)
        {
            partialViewName = $@"~/Widgets/{groupName}/{appName}/{partialViewName}/Widget.cshtml";
            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     显示Widget的路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="groupName">分组名称</param>
        /// <param name="appName">APP应用名称</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="areaName">区域名称</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> Widget(this IHtmlHelper html, string groupName, string appName,
            string partialViewName, string areaName)
        {
            partialViewName = $@"~/Widgets/{groupName}/{appName}/{partialViewName}/Widget.cshtml";
            //ViewDataDictionary viewData = new ViewDataDictionary();

            //return html.PartialAsync(partialViewName, viewData);

            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     左边菜单导航
        /// </summary>
        /// <param name="html"></param>
        /// <param name="sideBarType">菜单类型</param>
        public static Task<IHtmlContent> SideBar(this IHtmlHelper html, long fatherId)
        {
            return html.PartialAsync(@"~/Admin/Admin_SideBar.cshtml", fatherId);
        }

        /// <summary>
        ///     左边菜单导航
        /// </summary>
        /// <param name="html"></param>
        /// <param name="sideBarType">菜单类型</param>
        public static Task<IHtmlContent> SideBar(this IHtmlHelper html, SideBarType sideBarType)
        {
            var dic = new Dictionary<string, object>
            {
                {"SideBarType", sideBarType}
            };
            return html.PartialAsync(@"~/Admin/Admin_SideBar.cshtml", sideBarType);
        }

        /// <summary>
        ///     搜索
        /// </summary>
        /// <param name="html"></param>
        /// <param name="type">类型</param>
        public static Task<IHtmlContent> Search(this IHtmlHelper html, object type)
        {
            // 如果是分页类型

            return html.PartialAsync(@"~/Admin/Base/ViewSearch.cshtml", type);
        }

        /// <summary>
        ///     后台视图路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">组名</param>
        /// <param name="app">应用名称</param>
        /// <param name="partialViewName">视图名称</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string group, string app,
            string partialViewName)
        {
            partialViewName = $@"~/Admin/{group}/{app}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     后台视图路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">组名</param>
        /// <param name="app">应用名称</param>
        /// <param name="partialViewName">视图名称</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string group, string app,
            string partialViewName, object model)
        {
            partialViewName = $@"~/Admin/{group}/{app}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName, model);
        }

        /// <summary>
        ///     Admins the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string group, string partialViewName)
        {
            partialViewName = $@"~/Admin/{group}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     Admins the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string partialViewName)
        {
            partialViewName = $@"~/Admin/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName);
        }

        /// <summary>
        ///     Admins the widget from cache.
        ///     从缓存中获取视图
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="partialViewName">Partial name of the 视图.</param>
        public static IHtmlContent AdminWidgetFromCache(this IHtmlHelper html, string partialViewName)
        {
            var cacheKey = "AdminWidget" + partialViewName;
            var objectCache = Ioc.Resolve<IObjectCache>();
            if (!objectCache.TryGet(cacheKey, out IHtmlContent htmlContent))
            {
                var viewName = $@"~/Admin/{partialViewName}.cshtml";
                htmlContent = html.Partial(viewName);
                objectCache.Set(cacheKey, htmlContent);
            }

            return htmlContent;
        }

        /// <summary>
        ///     Admins the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string group, string partialViewName,
            object model)
        {
            partialViewName = $@"~/Admin/{group}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName, model);
        }

        /// <summary>
        ///     Admins the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdminWidget(this IHtmlHelper html, string group, string partialViewName,
            ViewDataDictionary viewData)
        {
            partialViewName = $@"~/Admin/{group}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName, viewData);
        }

        /// <summary>
        ///     动态获取高级编辑器，以及动态绑定数据
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="viewData">The view data.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> AdvanceEditor(this IHtmlHelper html, ViewDataDictionary viewData)
        {
            var partialViewName = @"~/Admin/Core/Common/AdvanceEditorControler.cshtml";
            return html.PartialAsync(partialViewName, viewData);
        }

        /// <summary>
        ///     Users the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="app">The application.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> UserWidget(this IHtmlHelper html, string group, string app,
            string partialViewName, object model)
        {
            partialViewName = $@"~/Views/{group}/{app}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName, model);
        }

        /// <summary>
        ///     Users the widget.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="group">The group.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> UserWidget(this IHtmlHelper html, string group, string partialViewName,
            object model)
        {
            partialViewName = $@"~/Views/{group}/{partialViewName}.cshtml";
            return html.PartialAsync(partialViewName, model);
        }

        /// <summary>
        ///     显示widget路径
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="partialViewName">Partial name of the view.</param>
        /// <param name="model">The model.</param>
        /// <returns>Task&lt;IHtmlContent&gt;.</returns>
        public static Task<IHtmlContent> Widget(this IHtmlHelper html, string partialViewName, object model)
        {
            partialViewName = $@"~/Widgets/{partialViewName}/Widget.cshtml";
            return html.PartialAsync(partialViewName, model);
        }
    }
}