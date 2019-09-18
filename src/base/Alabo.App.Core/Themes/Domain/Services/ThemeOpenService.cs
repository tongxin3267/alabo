using System;
using System.Collections.Generic;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.RestfulApi.Clients;

namespace Alabo.App.Core.Themes.Domain.Services {

    public class ThemeOpenService : ServiceBase, IThemeOpenService {

        public ThemeOpenService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        /// 站点发布
        /// </summary>
        /// <param name="themePublish"></param>
        /// <returns></returns>
        public ServiceResult Publish(ThemePublish themePublish) {
            if (themePublish.Theme == null) {
                return ServiceResult.FailedWithMessage("模板不能为空");
            }

            //if (!themePublish.Tenant.Equals(HttpWeb.Tenant, StringComparison.CurrentCultureIgnoreCase)) {
            //    return ServiceResult.FailedWithMessage("租户设置错误");
            //}

            // 模板不存在则添加模板
            var theme = Resolve<IThemeService>().GetSingle(r => r.Id == themePublish.Theme.Id);
            if (theme == null) {
                // 和服务器ID模板一样
                theme = new Theme();
                theme.Id = themePublish.Theme.Id;
                Resolve<IThemeService>().Add(theme);
            }

            theme = themePublish.Theme;
            theme.UpdateTime = DateTime.Now;
            if (!Resolve<IThemeService>().Update(theme)) {
                return ServiceResult.FailedWithMessage("模板更新失败");
            }

            //修改默认模板
            Resolve<IThemeService>().SetDefaultTheme(theme.Id);

            var addList = new List<ThemePage>();
            foreach (var themePage in themePublish.PageList) {
                addList.Add(themePage);
            }
            if (addList.Count > 0) {
                Resolve<IThemePageService>().DeleteMany(r => r.ThemeId == themePublish.Theme.Id);
                Resolve<IThemePageService>().AddMany(addList);
            }

            // 清除缓存
            var cacheKey = $"AllThemePageInfo_{themePublish.Theme.ClientType.ToString()}";
            ObjectCache.Remove(cacheKey);
            cacheKey = $"AllThemePageInfo_{theme.Type.ToStr()}_{theme.ClientType.ToStr()}";
            ObjectCache.Remove(cacheKey);
            cacheKey = $"AllThemePageInfo_{themePublish.Theme.Id.ToString()}";
            ObjectCache.Remove(cacheKey);

            return ServiceResult.Success;
        }

        /// <summary>
        /// 从远程获取模板
        /// </summary>
        /// <param name="themePageInput"></param>
        /// <returns></returns>
        public Theme InitThemeFormServcie(ClientPageInput themePageInput) {
            var apiUrl = "Api/Make/Init";
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                {"siteId",HttpWeb.Site.Id.ToString()},
                {"clientType",  themePageInput.ClientType.ToString()},
                {"type",  themePageInput.Type.ToString()}
            };

            var themePublish = Ioc.Resolve<IOpenClient>().Get<ThemePublish>(apiUrl, parameters);
            if (themePublish != null) {
                Publish(themePublish);
                var theme = Resolve<IThemeService>().GetSingle(themePublish.Theme.Id);
                return theme;
            } else {
                return null;
            }
        }
    }
}