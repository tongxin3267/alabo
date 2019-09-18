using Alabo.App.Core.UserType.Modules.City;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    /// </summary>
    public interface ICityService : IService {

        /// <summary>
        ///     查询城市中心列表
        /// </summary>
        /// <param name="query"></param>
        PagedList<CityView> GetPageList(object query);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        CityView GetView(long id);

        /// <summary>
        /// </summary>
        /// <param name="cityView"></param>
        ServiceResult AddOrUpdate(CityView cityView);

        /// <summary>
        ///     删除城市合伙人
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceResult Delete(long id);

        /// <summary>
        ///     根据区域获取城市代理
        /// </summary>
        /// <param name="regionId"></param>
        User.Domain.Entities.User GetCityUserType(long regionId);
    }
}