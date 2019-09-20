using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Operate.Domain.Service {

    /// <summary>
    /// 数据运维服务
    /// </summary>
    public interface IDataService : IService {

        /// <summary>
        /// 更新所有会员的组织架构图
        /// </summary>
        void UpdateAllUserMap();

        /// <summary>
        /// 检查所有的会员关系图是否正确
        /// </summary>

        ServiceResult CheckOutUserMap();
    }
}