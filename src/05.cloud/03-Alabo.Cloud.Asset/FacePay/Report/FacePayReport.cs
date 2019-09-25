using Alabo.App.Market.FacePay.Domain.Services;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoReports;
using System.Collections.Generic;
using System.Linq;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Enums;

namespace Alabo.App.Market.FacePay.Report {

    public class FacePayReport : IAutoReport {

        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel) {
            var list = Ioc.Resolve<IFacePayService>().GetList();
            var gpList = list.GroupBy(x => x.CreateTime.Date);

            var chartCols = new List<string> { "日期", "支付笔数", "金额" };
            var chartRows = new List<object>();
            foreach (var gp in gpList) {
                var item = new {
                    日期 = gp.Key.ToString("yyyy-MM-dd"),
                    支付笔数 = gp.Count(),
                    金额 = gp.Sum(x => x.Amount),
                };

                chartRows.Add(item);
            }
            return new List<AutoReport>
            {
                new AutoReport{Name = "数据走势图", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
                new AutoReport{Name = "数据分布图", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Pie, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}