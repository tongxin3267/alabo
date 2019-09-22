using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Themes.Domain.Services {

    /// <summary>
    ///     数据保存
    /// </summary>
    public interface IThemeOpenService : IService {

        /// <summary>
        /// 站点发布
        /// </summary>
        /// <param name="themePublish"></param>
        /// <returns></returns>
        ServiceResult Publish(ThemePublish themePublish);

        /// <summary>
        /// 重复器上获取默认模板
        /// </summary>
        /// <param name="themePageInput"></param>
        /// <returns></returns>
        Theme InitThemeFormServcie(ClientPageInput themePageInput);
    }
}