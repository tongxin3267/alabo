using System;
using Alabo.App.Core.UserType.Modules.ServiceCenter;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    ///     服务中心
    ///     线下门店
    /// </summary>
    public interface IServiceCenterService : IService {

        /// <summary>
        ///     查询门店列表
        /// </summary>
        /// <param name="query"></param>
        PagedList<ServiceCenterView> GetPageList(object query);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceCenterView GetView(long id);

        /// <summary>
        /// </summary>
        /// <param name="serviceCenterView"></param>
        ServiceResult AddOrUpdate(ServiceCenterView serviceCenterView);

        /// <summary>
        ///     删除城市合伙人
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceResult Delete(long id);

        /// <summary>
        ///     根据用户Id，获取服务中心
        /// </summary>
        /// <param name="userId">用户Id</param>
        Tuple<Entities.UserType, User.Domain.Entities.User> GetServiceUser(long userId);

        /// <summary>
        ///     获取服务中心或门店的
        ///     推荐用户和推荐服务中心
        /// </summary>
        /// <param name="parentUserId">服务中心推荐用户Id</param>
        Tuple<Entities.UserType, User.Domain.Entities.User> GetServiceParentUser(long parentUserId);

        /// <summary>
        ///     会员升级自动升级为服务中心，在UserType中添加一条记录
        ///     更新下面所有用户的ServiceCenterId
        /// </summary>
        /// <param name="userId">当前要升级的用户Id</param>
        /// <param name="isSelf">当期升级会员，serviceUserId是否==UserID</param>
        void UserUpgradServiceCenter(long userId, bool isSelf = true);
    }
}