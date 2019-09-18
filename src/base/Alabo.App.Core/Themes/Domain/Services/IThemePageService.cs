using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Domains.Services;
using Alabo.Validations.Aspects;

namespace Alabo.App.Core.Themes.Domain.Services {

    public interface IThemePageService : IService<ThemePage, ObjectId> {
        /// <summary>
        ///     根据客户端类型，获取所有的模板信息
        ///     用户客户端缓存
        /// </summary>

        AllClientPages GetAllClientPages([Valid]ClientPageInput themePageInput);
    }
}