using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Alabo.App.Agent.Citys.Domain.Entities;
using Alabo.App.Agent.Citys.Domain.Services;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Reflections.Interfaces;
using Alabo.Helpers;

namespace Alabo.App.Agent.Citys {

    public class CityDefault : IDefaultInit {
        public bool IsTenant => false;

        public void Init() {

            #region 添加Admin为默认员工

            var user = Ioc.Resolve<IUserService>().GetSingle("admin");
            if (user != null) {
                var find = Ioc.Resolve<ICityService>().GetSingle(r => r.UserId == user.Id);
                if (find == null) {
                    City city = new City {
                        Name = user.GetUserName(),
                        UserId = user.Id,
                        RegionId = 0,
                    };
                    Ioc.Resolve<ICityService>().Add(city);
                }
            }

            #endregion 添加Admin为默认员工
        }
    }
}