using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Tool.Payment.WeiXinMp.Models;

namespace Alabo.Tool.Payment.WeiXinMp.Services {

    /// <summary>
    /// 微信公众号相关接口
    /// </summary>
    public class WeixinMpService : ServiceBase, IWeixinMpService {

        public WeixinMpService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public WeiXinShare WeiXinShare(WeiXinShareInput shareInput) {
            //var wexinConfig = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();

            //var jsApiTicketResult = Ioc.Resolve<IWeixinMpClient>().GetTicketByAccessToken();
            //if (jsApiTicketResult == null) {
            //    Resolve<ITableService>().Log("tickets获取失败");
            //    throw new ValidException("tickets获取失败");
            //}

            //var webSiteConfig = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            //if (CollectionExtensions.IsNullOrEmpty(shareInput.Title)) {
            //    shareInput.Title = webSiteConfig.WebSiteName;
            //}

            //if (CollectionExtensions.IsNullOrEmpty(shareInput.Description)) {
            //    shareInput.Description = webSiteConfig.Description;
            //}
            //if (CollectionExtensions.IsNullOrEmpty(shareInput.ImageUrl)) {
            //    shareInput.ImageUrl = Resolve<IApiService>().ApiImageUrl(webSiteConfig.Logo);
            //}
            //WeiXinShare share = new WeiXinShare {
            //    AppId = wexinConfig.AppId,
            //    Timestamp = JSSDKHelper.GetTimestamp(),
            //    Link = shareInput.Url,
            //    Ticket = jsApiTicketResult.ticket,
            //    ImageUrl = shareInput.ImageUrl,
            //    Title = shareInput.Title,
            //    Description = shareInput.Description,
            //    NonceStr = JSSDKHelper.GetNoncestr()
            //};
            //share.Signature = JSSDKHelper.GetSignature(jsApiTicketResult.ticket, share.NonceStr, share.Timestamp,
            //    share.Link);
            //Resolve<ITableService>().Log("微信分享" + share.ToJson());
            //return share;
            return null;
        }
    }
}