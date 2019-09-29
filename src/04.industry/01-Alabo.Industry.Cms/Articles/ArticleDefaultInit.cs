using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Industry.Cms.Articles.Domain.Services;

namespace Alabo.Industry.Cms.Articles
{
    public class ArticleDefaultInit : IDefaultInit
    {
        public bool IsTenant => false;

        public void Init()
        {
            //频道数据初始化
            Ioc.Resolve<IChannelService>().InitialData();

            //关于我们
            Ioc.Resolve<IAboutService>().InitialData();
        }
    }
}