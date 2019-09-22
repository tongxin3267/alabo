using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alabo.App.Core.Admin;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.Themes.Domain.Enums;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Helpers;
using Alabo.Initialize;

namespace Alabo.App.Shop.Store {

    public class StoreDefault : IDefaultInit {
        public bool IsTenant => false;

        /// <summary>
        /// 初始化 供应商员工以及权限
        /// </summary>
        public void Init() {
            Ioc.Resolve<IStoreService>().PlanformStore();
        }
    }
}