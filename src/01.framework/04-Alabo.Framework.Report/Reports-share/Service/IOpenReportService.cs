using Alabo.App.Share.Reports.Admin;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;

namespace Alabo.App.Share.Reports.Service {

    public interface IOpenReportService : IService {
        /// <summary>
        /// 获取左侧菜单统计数据
        /// </summary>
        /// <param name="sideBarType"></param>
        /// <param name="fatherId"></param>

        AdminSideBarReport GetAdminSideBarReport(SideBarType sideBarType, long fatherId);
    }
}