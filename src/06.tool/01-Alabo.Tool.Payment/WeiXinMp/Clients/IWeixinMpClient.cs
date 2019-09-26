using Alabo.Dependency;
using Senparc.Weixin.MP.Entities;

namespace Alabo.Tool.Payment.WeiXinMp.Clients
{
    /// <summary>
    ///     微信公众号相关接口
    /// </summary>
    public interface IWeixinMpClient : IScopeDependency
    {
        /// <summary>
        ///     获取凭证接口
        /// </summary>
        AccessTokenResult GetToken();

        /// <summary>
        ///     用户信息接口
        /// </summary>
        /// <param name="openId">AccessToken或AppId（推荐使用AppId，需要先注册）</param>
        /// <returns></returns>
        WeixinUserInfoResult GetUserInfo(string openId);

        /// <summary>
        ///     获取调用微信JS接口的临时票据
        /// </summary>
        /// <returns></returns>
        JsApiTicketResult GetTicketByAccessToken();
    }
}