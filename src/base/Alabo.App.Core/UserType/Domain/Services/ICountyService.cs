using Alabo.App.Core.UserType.Modules.County;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    /// </summary>
    public interface ICountyService : IService {

        /// <summary>
        ///     查询城市中心列表
        /// </summary>
        /// <param name="query"></param>
        PagedList<CountyView> GetPageList(object query);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceResult Delete(long id);

        /// <summary>
        /// </summary>
        /// <param name="CountyView"></param>
        ServiceResult AddOrUpdate(CountyView CountyView);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        CountyView GetView(long id);
    }
}