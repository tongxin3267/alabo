using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using Alabo.Industry.Shop.Deliveries.Domain.Services;

namespace Alabo.Industry.Shop.Deliveries {

    public class StoreDefault : IDefaultInit {
        public bool IsTenant => false;

        /// <summary>
        /// 初始化 供应商员工以及权限
        /// </summary>
        public void Init() {
            Ioc.Resolve<IShopStoreService>().PlanformStore();
        }
    }
}