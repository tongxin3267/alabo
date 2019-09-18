using Senparc.Weixin.MP.Helpers;
using System;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.ApiStore.WeiXinMp.Clients;
using Alabo.App.Core.ApiStore.WeiXinMp.Models;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using CollectionExtensions = Castle.Core.Internal.CollectionExtensions;

namespace Alabo.App.Core.ApiStore.WeiXinMp.Services {

    /// <summary>
    /// 微信公众号相关接口
    /// </summary>
    public class WeixinMpService : ServiceBase, IWeixinMpService {

        public WeixinMpService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public WeiXinShare WeiXinShare(WeiXinShareInput shareInput) {
            var wexinConfig = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();

            var jsApiTicketResult = Ioc.Resolve<IWeixinMpClient>().GetTicketByAccessToken();
            if (jsApiTicketResult == null) {
                Resolve<ITableService>().Log("tickets获取失败");
                throw new ValidException("tickets获取失败");
            }

            var webSiteConfig = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            if (CollectionExtensions.IsNullOrEmpty(shareInput.Title)) {
                shareInput.Title = webSiteConfig.WebSiteName;
            }

            if (CollectionExtensions.IsNullOrEmpty(shareInput.Description)) {
                shareInput.Description = webSiteConfig.Description;
            }
            if (CollectionExtensions.IsNullOrEmpty(shareInput.ImageUrl)) {
                shareInput.ImageUrl = Resolve<IApiService>().ApiImageUrl(webSiteConfig.Logo);
            }
            WeiXinShare share = new WeiXinShare {
                AppId = wexinConfig.AppId,
                Timestamp = JSSDKHelper.GetTimestamp(),
                Link = shareInput.Url,
                Ticket = jsApiTicketResult.ticket,
                ImageUrl = shareInput.ImageUrl,
                Title = shareInput.Title,
                Description = shareInput.Description,
                NonceStr = JSSDKHelper.GetNoncestr()
            };
            share.Signature = JSSDKHelper.GetSignature(jsApiTicketResult.ticket, share.NonceStr, share.Timestamp,
                share.Link);
            Resolve<ITableService>().Log("微信分享" + share.ToJson());
            return share;
        }
    }
}