using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Themes.Domain.Services {

    public interface IThemeService : IService<Theme, ObjectId> {

        /// <summary>
        /// 获取默认模板
        /// </summary>
        /// <param name="clientType"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientType clientType);

        /// <summary>
        /// 默认模板Id
        /// </summary>
        /// <param name="themePageInput"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientPageInput themePageInput);

        /// <summary>
        /// 初始化模板
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        void InitTheme(string objectId);

        /// <summary>
        /// 设置默认模板
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        bool SetDefaultTheme(ObjectId themeId);
    }
}