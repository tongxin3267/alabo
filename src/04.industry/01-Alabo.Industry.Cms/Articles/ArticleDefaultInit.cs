using System.Threading.Tasks;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Core.Reflections.Interfaces;
using Alabo.Helpers;

namespace Alabo.App.Cms.Articles {

    public class ArticleDefaultInit : IDefaultInit {
        public bool IsTenant => false;

        public void Init() {
            //频道数据初始化
            Ioc.Resolve<IChannelService>().InitialData();

            //关于我们
            Ioc.Resolve<IAboutService>().InitialData();
        }
    }
}