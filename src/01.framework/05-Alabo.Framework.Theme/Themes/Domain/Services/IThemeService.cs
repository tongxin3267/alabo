using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Themes.Domain.Services {

    public interface IThemeService : IService<Theme, ObjectId> {

        /// <summary>
        /// ��ȡĬ��ģ��
        /// </summary>
        /// <param name="clientType"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientType clientType);

        /// <summary>
        /// Ĭ��ģ��Id
        /// </summary>
        /// <param name="themePageInput"></param>
        /// <returns></returns>
        Theme GetDefaultTheme(ClientPageInput themePageInput);

        /// <summary>
        /// ��ʼ��ģ��
        /// </summary>
        /// <param name="objectId"></param>
        /// <returns></returns>
        void InitTheme(string objectId);

        /// <summary>
        /// ����Ĭ��ģ��
        /// </summary>
        /// <param name="themeId"></param>
        /// <returns></returns>
        bool SetDefaultTheme(ObjectId themeId);
    }
}