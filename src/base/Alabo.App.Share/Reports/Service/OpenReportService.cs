using System.Linq;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Open.Reports.Admin;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;

namespace Alabo.App.Open.Reports.Service {

    public class OpenReportService : ServiceBase, IOpenReportService {

        public OpenReportService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public AdminSideBarReport GetAdminSideBarReport(SideBarType sideBarType, long fatherId) {
            var sideBarReport = new AdminSideBarReport();
            var adminSideBarReports = Resolve<IReportService>().GetList<Alabo.App.Open.Reports.Admin.AdminSideBarReport>();
            sideBarReport = adminSideBarReports.FirstOrDefault(r => r.SideBarType == sideBarType);
            return sideBarReport;
        }
    }
}