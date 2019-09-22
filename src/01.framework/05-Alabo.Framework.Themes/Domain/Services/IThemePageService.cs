using MongoDB.Bson;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Domains.Services;
using Alabo.Validations.Aspects;

namespace Alabo.App.Core.Themes.Domain.Services {

    public interface IThemePageService : IService<ThemePage, ObjectId> {
        /// <summary>
        ///     ���ݿͻ������ͣ���ȡ���е�ģ����Ϣ
        ///     �û��ͻ��˻���
        /// </summary>

        AllClientPages GetAllClientPages([Valid]ClientPageInput themePageInput);
    }
}