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

    public class BillReport : IAutoReport {

        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            //总账单额度
            var sqlTotalBill = $@" select sum(Amount) from [dbo].[Asset_Bill]   ";
            var TotalBill = dbContext.ExecuteScalar(sqlTotalBill);
            //最近总账单额度
            var sqlNewTotalBill = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=3";
            var NewTotalBill = dbContext.ExecuteScalar(sqlNewTotalBill);
            //最近七天总账单额度
            var sqlWeekTotalBill = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=7";
            var WeekTotalBill = dbContext.ExecuteScalar(sqlWeekTotalBill);
            //最近一个月总账单额度
            var sqlMonthTotalBill = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=31";
            var MonthTotalBill = dbContext.ExecuteScalar(sqlMonthTotalBill);

            //总充值金额
            var sqlTotalRecharge = $@" select sum(Amount) from [dbo].[Asset_Bill]   ";
            var TotalRecharge = dbContext.ExecuteScalar(sqlTotalRecharge);
            //总提现金额
            var sqlTotalCash = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=3";
            var TotalCash = dbContext.ExecuteScalar(sqlTotalCash);
            //总消费金额
            var sqlTotalConsume = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=7";
            var TotalConsume = dbContext.ExecuteScalar(sqlTotalConsume);
            //总销售金额
            var sqlTotalSale = $@"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=31";
            var TotalSale = dbContext.ExecuteScalar(sqlTotalSale);

            //多维度分析
            var sqlCountByDay = $@" SELECT CONVERT(VARCHAR(100), CreateTime, 23) Day
                                    ,COUNT(CONVERT(VARCHAR(100), CreateTime, 23)) COUNT
                                    ,sum(Amount) as TotalAmout,
                                    sum([AfterAmount]) as TotalAfterAmount,
                                    SUM(CASE WHEN type = 201 THEN Amount ELSE 0 END) AS TotalConsume,
                                    SUM(CASE WHEN type = 110 THEN Amount ELSE 0 END) AS TotalSale
                                    from [dbo].[Asset_Bill]
                                    GROUP BY CONVERT(VARCHAR(100), CreateTime, 23) ";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "总账单额度",Intro=TotalBill.ToString(),Value = TotalBill.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "最近总账单额度",Intro=NewTotalBill.ToString(), Value =NewTotalBill.ToString().ToDecimal(), Icon = "aa2.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "最近七天总账单额度",Intro=WeekTotalBill.ToString(), Value = WeekTotalBill.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "最近一个月总账单额度",Intro=MonthTotalBill.ToString(), Value = MonthTotalBill.ToString().ToDecimal(), Icon = "aa4.ico"},

                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "总充值金额",Intro=TotalRecharge.ToString(),Value = TotalRecharge.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "总提现金额",Intro=TotalCash.ToString(), Value =TotalCash.ToString().ToDecimal(), Icon = "aa2.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "总消费金额",Intro=TotalConsume.ToString(), Value = TotalConsume.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "总销售金额",Intro=TotalSale.ToString(), Value = TotalSale.ToString().ToDecimal(), Icon = "aa4.ico"},
            };
            var chartCols = new List<string> { "日期", "总账户额度", "帐单进出次数", "消费额度", "销售额度", "变动后账户额度" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                while (dr.Read()) {
                    var item = new {
                        日期 = dr["Day"].ToString(),
                        总账户额度 = dr["TotalAmout"].ToString(),
                        帐单进出次数 = dr["Count"].ToString(),
                        消费额度 = dr["TotalConsume"].ToString(),
                        销售额度 = dr["TotalSale"].ToString(),
                        变动后账户额度 = dr["TotalAfterAmount"].ToString(),
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport{ Name = "账户概况", Icon = "Report1.icon", AutoReprotData = new AutoReprotData{ Type = ReportDataType.Amount ,  List = dataList }},
                new AutoReport{ Name = "数据走势图", Icon = "Report2.icon", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}