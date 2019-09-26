using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis.Design.AutoReports;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Enums;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;

namespace Alabo.App.Core.User.UI {

    public class InterfaceReport : IAutoReport {

        /// <summary>
        /// AutoReport 结果返回
        /// </summary>
        /// <returns></returns>
        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var sqlMemberCount = $@" SELECT COUNT(*) FROM Basic_Record  ";
            var memberCount = dbContext.ExecuteScalar(sqlMemberCount);
            var sqlNewOrder = $@" SELECT COUNT(*) FROM Basic_Record WHERE CreateTime > '{DateTime.Now.Date.ToString("yyyyMMdd")}'";
            var newOrderCount = dbContext.ExecuteScalar(sqlNewOrder);

            var sqlCountByDay = $@"SELECT CONVERT(VARCHAR(100), CreateTime, 23) Day
	                                    ,COUNT(CONVERT(VARCHAR(100), CreateTime, 23)) COUNT
                                    FROM Basic_Record
                                    GROUP BY CONVERT(VARCHAR(100), CreateTime, 23) ";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "新订单",Intro=newOrderCount.ToString(), Value = newOrderCount.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "跳出率",Intro=newOrderCount.ToString(), Value =newOrderCount.ToString().ToDecimal(), Icon = "aa2.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "用户注册数",Intro=memberCount.ToString(), Value = memberCount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "访客",Intro=memberCount.ToString(), Value = memberCount.ToString().ToDecimal(), Icon = "aa4.ico"},
            };
            var chartCols = new List<string> { "日期", "访问用户", "下单用户" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                while (dr.Read()) {
                    var item = new {
                        日期 = dr["Day"].ToString(),
                        访问用户 = dr["Count"].ToString(),
                        下单用户 = dr["Count"].ToString(),
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport{ Name = "端口统计", Icon = "Report1.icon", AutoReprotData = new AutoReprotData{ Type = ReportDataType.Amount ,  List = dataList }},
                new AutoReport{ Name = "数据走势图", Icon = "Report2.icon", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}