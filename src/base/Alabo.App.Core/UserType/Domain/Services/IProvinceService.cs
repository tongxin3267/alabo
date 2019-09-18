using Alabo.App.Core.UserType.Modules.Province;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    /// </summary>
    public interface IProvinceService : IService {

        /// <summary>
        ///     查询城市中心列表
        /// </summary>
        /// <param name="query"></param>
        PagedList<ProvinceView> GetPageList(object query);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceResult Delete(long id);

        /// <summary>
        /// </summary>
        /// <param name="ProvinceView"></param>
        ServiceResult AddOrUpdate(ProvinceView ProvinceView);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        ProvinceView GetView(long id);
    }
}