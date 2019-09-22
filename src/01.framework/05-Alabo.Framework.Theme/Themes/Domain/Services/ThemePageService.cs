using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.App.Core.Themes.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.Themes.Domain.Services {

    public class ThemePageService : ServiceBase<ThemePage, ObjectId>, IThemePageService {

        public ThemePageService(IUnitOfWork unitOfWork, IRepository<ThemePage, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        ///     ���ݿͻ������ͣ���ȡ���е�ģ����Ϣ
        ///     �û��ͻ��˻���
        /// </summary>
        /// <param name="themePageInput"></param>
        public AllClientPages GetAllClientPages(ClientPageInput themePageInput) {
            var cacheKey = $"AllThemePageInfo_{themePageInput.Type.ToStr()}_{themePageInput.ClientType.ToStr()}";
            return ObjectCache.GetOrSet(() => {
                AllClientPages allClientPages = new AllClientPages();
                var defaultTheme = Resolve<IThemeService>().GetDefaultTheme(themePageInput);
                if (defaultTheme == null) {
                    throw new ValidException("Ĭ��ģ��Ϊ��");
                }

                var themePage = GetList(r => r.ThemeId == defaultTheme.Id);
                var resultList = new List<ClientPage>();
                foreach (var page in themePage) {
                    resultList.Add(page.ToClientPage());
                }

                // ��ֵ
                allClientPages.Theme.Setting = defaultTheme.Setting.ToObject<object>();
                allClientPages.Theme.Type = defaultTheme.Type;
                allClientPages.Theme.Id = defaultTheme.Id.ToString();
                allClientPages.Theme.Name = defaultTheme.Name;
                allClientPages.Theme.Menu = defaultTheme.Menu;
                allClientPages.Theme.UpdateTime = defaultTheme.UpdateTime;
                allClientPages.Theme.TabBarSetting = defaultTheme.TabBarSetting.ToObject<object>();

                allClientPages.PageList = resultList;

                allClientPages.Site.Name = HttpWeb.Site.CompanyName;
                allClientPages.Site.Tenant = HttpWeb.Tenant;

                // �ж��Ƿ���΢��֧��
                var config = Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
                if (!config.IsEnable || config.AppId.IsNullOrEmpty() || config.APISecretKey.IsNullOrEmpty() || config.CallBackUrl.IsNullOrEmpty()) {
                    allClientPages.Site.IsWeiXinPay = false;
                } else {
                    allClientPages.Site.IsWeiXinPay = true;
                    var scope = "snsapi_userinfo";
                    if (!config.IsBaseUserInfo) {
                        scope = "snsapi_base";
                    }
                    allClientPages.Site.WeiXinUrl =
                        $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={config.AppId}&redirect_uri={config.ReturnUrl}&response_type=code&scope={scope}&state=STATE#wechat_redirect";
                }
                return allClientPages;
            }, cacheKey).Value;
        }
    }
}