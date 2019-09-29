using Alabo.Domains.Services;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Dtos;
using Alabo.Validations.Aspects;
using MongoDB.Bson;

namespace Alabo.Framework.Themes.Domain.Services
{
    public interface IThemePageService : IService<ThemePage, ObjectId>
    {
        /// <summary>
        ///     根据客户端类型，获取所有的模板信息
        ///     用户客户端缓存
        /// </summary>
        AllClientPages GetAllClientPages([Valid] ClientPageInput themePageInput);
    }
}