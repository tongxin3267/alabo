using Alabo.App.Core.ApiStore.WeiXinMp.Models;
using Alabo.Domains.Services;

namespace Alabo.App.Core.ApiStore.WeiXinMp.Services {

    public interface IWeixinMpService : IService {

        /// <summary>
        /// 微信分享
        /// </summary>
        /// <param name="shareInput"></param>
        /// <returns></returns>
        WeiXinShare WeiXinShare(WeiXinShareInput shareInput);
    }
}