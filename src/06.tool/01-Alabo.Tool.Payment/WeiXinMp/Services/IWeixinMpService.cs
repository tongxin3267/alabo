using Alabo.Domains.Services;
using Alabo.Tool.Payment.WeiXinMp.Models;

namespace Alabo.Tool.Payment.WeiXinMp.Services
{
    public interface IWeixinMpService : IService
    {
        /// <summary>
        ///     微信分享
        /// </summary>
        /// <param name="shareInput"></param>
        /// <returns></returns>
        WeiXinShare WeiXinShare(WeiXinShareInput shareInput);
    }
}