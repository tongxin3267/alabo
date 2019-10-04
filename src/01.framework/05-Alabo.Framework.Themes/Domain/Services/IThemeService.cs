using System.Collections.Generic;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.PostRoles.Dtos;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Dtos;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Domain.Services
{
    public interface IThemeService : IService<Theme, ObjectId>
    {
        /// <summary>
        ///     获取默认模板
        /// </summary>
        /// <param name="clientType"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientType clientType);

        /// <summary>
        ///     默认模板Id
        /// </summary>
        /// <param name="themePageInput"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientPageInput themePageInput);

        /// <summary>
        ///     初始化模板
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        void InitTheme(string objectId);

        /// <summary>
        ///     设置默认模板
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        bool SetDefaultTheme(ObjectId themeId);

        /// <summary>
        ///     获取后台管理模板菜单
        ///      在管理权限中会动态调用
        /// </summary>
        /// <returns></returns>
        List<ThemeOneMenu> GetAdminThemeMenus();
    }
}