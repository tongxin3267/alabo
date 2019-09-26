using Alabo.Data.People.Cities.Domain.Entities;
using Alabo.Data.People.Cities.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;

namespace Alabo.Data.People.Cities {

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