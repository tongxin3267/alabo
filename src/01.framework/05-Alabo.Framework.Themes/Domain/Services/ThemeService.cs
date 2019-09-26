using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Clients;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Dtos;
using Alabo.Helpers;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Services;

namespace Alabo.Framework.Themes.Domain.Services
{
    public class ThemeService : ServiceBase<Theme, ObjectId>, IThemeService
    {
        private RestClientConfiguration _restClientConfiugration;
        private IServerAuthenticationManager _serverAuthenticationManager;
        private IThemeClient _themeClient;

        public ThemeService(IUnitOfWork unitOfWork, IRepository<Theme, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     ��ȡĬ��ģ��
        /// </summary>
        /// <param name="clientType"></param>
        public Theme GetDefaultTheme(ClientType clientType)
        {
            var theme = GetSingle(r => r.ClientType == clientType && r.IsDefault);
            if (theme == null)
            {
                theme = GetSingle(r => r.ClientType == clientType);
                if (theme != null)
                {
                    theme.IsDefault = true;
                    Update(theme);
                }
            }

            return theme;
        }

        public Theme GetDefaultTheme(ClientPageInput themePageInput)
        {
            // ��ȡĬ��ģ��
            Theme defaultTheme = null;
            if (themePageInput.ClientType == ClientType.PcWeb)
                defaultTheme = GetSingle(r =>
                    r.ClientType == themePageInput.ClientType && r.Type == themePageInput.Type && r.IsDefault);
            else
                defaultTheme = GetSingle(r => r.ClientType == themePageInput.ClientType && r.IsDefault);

            if (defaultTheme == null)
            {
                defaultTheme = GetSingle(r => r.ClientType == themePageInput.ClientType);
                if (defaultTheme != null)
                {
                    defaultTheme.IsDefault = true;
                    Update(defaultTheme);
                }
            }

            if (defaultTheme == null) // �ӷ������ϻ�ȡĬ��ģ��
                defaultTheme = Resolve<IThemeOpenService>().InitThemeFormServcie(themePageInput);

            return defaultTheme;
        }

        public void InitTheme(string objectId)
        {
            //Todo Token.ProjectId�����bug���⣬��ȡģ��
            var model = _themeClient.GetThemeAndThemePagesAsync(_serverAuthenticationManager.Token.Token,
                objectId.ToObjectId(), _serverAuthenticationManager.Token.ProjectId).GetAwaiter();
            if (model.GetResult() == null) throw new ValidException("ģ�����ݻ�ȡʧ��");

            var publish = model.GetResult();
            publish.Tenant = HttpWeb.Tenant;
            var result = Resolve<IThemeOpenService>().Publish(publish);
            if (!result.Succeeded) throw new ValidException("ģ�����ݲ���ʧ��");
        }

        public bool SetDefaultTheme(ObjectId themeId)
        {
            var find = GetSingle(themeId);
            if (find == null) return false;

            var list = GetList(r => r.Type == find.Type);
            foreach (var item in list)
            {
                if (item.Id == find.Id)
                    item.IsDefault = true;
                else
                    item.IsDefault = false;

                Update(item);
            }

            return false;
        }
    }
}