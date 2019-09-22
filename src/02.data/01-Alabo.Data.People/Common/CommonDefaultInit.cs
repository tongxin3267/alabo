using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Helpers;
using Alabo.Initialize;

namespace Alabo.App.Core.Common {

    public class CommonDefaultInit : IDefaultInit {
        public bool IsTenant => false;

        public void Init() {
            //导入地址数据 ,从js文件导入,如果从高德地图导入会导致地址库不同步的问题
            Ioc.Resolve<IRegionService>().InitRegion();

            //// 导入商圈数据
            //Ioc.Resolve<ICircleService>().Init();

            // 导入Schedule数据
            Ioc.Resolve<IScheduleService>().Init();

            // 初始化所有没有资产账号的会员
            Ioc.Resolve<IAccountService>().InitAllUserIdsWidthOutAccount();
        }
    }
}