using Alabo.App.Core.Themes.Dtos;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.Domains.Entities;
using Alabo.RestfulApi;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;

namespace Alabo.App.Core.Themes.Clients {

    public interface IThemeClient : IClient {

        /// <summary>
        /// 初始化服务端站点数据
        /// </summary>
        /// <param name="token"></param>
        /// <param name="proejctId"></param>
        /// <returns></returns>
        Task<ServiceResult> InitOpenSite(string token, Guid proejctId);

        /// <summary>
        /// 获取模板，和模板页面
        /// </summary>
        /// <returns></returns>
        Task<ThemePublish> GetThemeAndThemePagesAsync(string token, ObjectId themeId, Guid proejctId);

        /// <summary>
        /// 获取模板，和模板页面
        /// </summary>
        /// <returns></returns>
        ThemePublish GetThemeAndThemePages(string token, ObjectId themeId, Guid proejctId);

        /// <summary>
        /// 初始化终端模板
        /// </summary>
        /// <returns></returns>
        ThemePublish InitDefaultTheme(ClientPageInput clientPageInput);
    }
}