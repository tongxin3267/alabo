using Alabo.App.Core.UserType.Modules.Partner;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.UserType.Domain.Services {

    /// <summary>
    /// </summary>
    public interface IPartnerService : IService {

        /// <summary>
        ///     查询合伙人中心列表
        /// </summary>
        /// <param name="query"></param>
        PagedList<PartnerView> GetPageList(object query);

        /// <summary>
        /// </summary>
        /// <param name="id">主键ID</param>
        PartnerView GetView(long id);

        /// <summary>
        /// </summary>
        /// <param name="partnerView"></param>
        ServiceResult AddOrUpdate(PartnerView partnerView);

        /// <summary>
        ///     删除合伙人合伙人
        /// </summary>
        /// <param name="id">主键ID</param>
        ServiceResult Delete(long id);

        /// <summary>
        ///     根据区域获取合伙人代理
        /// </summary>
        /// <param name="regionId"></param>
        User.Domain.Entities.User GetPartnerUserType(long regionId);
    }
}