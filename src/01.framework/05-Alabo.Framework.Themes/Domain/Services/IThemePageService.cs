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
        ///     ���ݿͻ������ͣ���ȡ���е�ģ����Ϣ
        ///     �û��ͻ��˻���
        /// </summary>
        AllClientPages GetAllClientPages([Valid] ClientPageInput themePageInput);
    }
}