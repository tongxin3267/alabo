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

    public class AccountReport : IAutoReport {

        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            //总资产额度
            var sqlTotalAccount = $@" select sum(Amount) from [dbo].[Asset_Account] ";
            var TotalAccount = dbContext.ExecuteScalar(sqlTotalAccount);
            //最近总资产额度
            var sqlNewTotalAccount = $@"select sum(Amount) from [dbo].[Asset_Account] where  DateDiff(dd,CreateTime,getdate())<=3";
            var NewTotalAccount = dbContext.ExecuteScalar(sqlNewTotalAccount);
            //最近七天总资产额度
            var sqlWeekTotalAccount = $@"select sum(Amount) from [dbo].[Asset_Account] where  DateDiff(dd,CreateTime,getdate())<=7";
            var WeekTotalAccount = dbContext.ExecuteScalar(sqlWeekTotalAccount);
            //最近一个月资产单额度
            var sqlMonthTotalAccount = $@"select sum(Amount) from [dbo].[Asset_Account] where  DateDiff(dd,CreateTime,getdate())<=31";
            var MonthTotalAccount = dbContext.ExecuteScalar(sqlMonthTotalAccount);

            //人民币总额
            var sqlTotalCash = $@" select sum(Amount)  from [dbo].[Asset_Account]
                                    where  [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699000' ";
            var TotalCash = dbContext.ExecuteScalar(sqlTotalCash);
            //积分总额
            var sqlTotalIntegral = $@"select sum(Amount)  from [dbo].[Asset_Account]
                                where  [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699002'";
            var TotalIntegral = dbContext.ExecuteScalar(sqlTotalIntegral);
            //消费额总额
            var sqlTotalConsume = $@"select sum(Amount)  from [dbo].[Asset_Account]
                                        where  [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699756'";
            var TotalConsume = dbContext.ExecuteScalar(sqlTotalConsume);
            //优惠券总额
            var sqlTotalCoupon = $@"select sum(Amount)  from [dbo].[Asset_Account]
                                where  [MoneyTypeId]='e97ccd1e-1478-49bd-bfc7-e73a5d699009'";
            var TotalCoupon = dbContext.ExecuteScalar(sqlTotalCoupon);

            //多维度分析
            var sqlCountByDay = $@"SELECT
                                CONVERT(VARCHAR(100), CreateTime, 23) Day,
                                sum(Amount) as TotalAmout,
                                SUM(CASE WHEN [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699000' THEN Amount ELSE 0 END) AS TotalCash,
                                SUM(CASE WHEN [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699002' THEN Amount ELSE 0 END) AS TotalIntegral,
                                SUM(CASE WHEN [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699000' THEN Amount ELSE 0 END) AS TotalConsume,
                                SUM(CASE WHEN [MoneyTypeId]='E97CCD1E-1478-49BD-BFC7-E73A5D699000' THEN Amount ELSE 0 END) AS TotalCoupon
                                from [dbo].[Asset_Account]
                                GROUP BY CONVERT(VARCHAR(100), CreateTime, 23)
                                 ";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "总资产额度",Intro=TotalAccount.ToString(),Value = TotalAccount.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "最近总资产额度",Intro=NewTotalAccount.ToString(), Value =NewTotalAccount.ToString().ToDecimal(), Icon = "aa2.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "最近七天总资产额度",Intro=WeekTotalAccount.ToString(), Value = WeekTotalAccount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "最近一个月资产单额度",Intro=MonthTotalAccount.ToString(), Value = MonthTotalAccount.ToString().ToDecimal(), Icon = "aa4.ico"},

                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "人民币总额",Intro=TotalCash.ToString(),Value = TotalCash.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "积分总额",Intro=TotalIntegral.ToString(), Value =TotalIntegral.ToString().ToDecimal(), Icon = "aa2.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "消费额总额",Intro=TotalConsume.ToString(), Value = TotalConsume.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "优惠券总额",Intro=TotalCoupon.ToString(), Value = TotalCoupon.ToString().ToDecimal(), Icon = "aa4.ico"},
            };
            var chartCols = new List<string> { "日期", "资产额度", "人民币额度", "积分额度", "消费额额度", "优惠券额度" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                while (dr.Read()) {
                    var item = new {
                        日期 = dr["Day"].ToString(),
                        资产额度 = dr["TotalAmout"].ToString(),
                        人民币额度 = dr["TotalCash"].ToString(),
                        积分额度 = dr["TotalIntegral"].ToString(),
                        消费额额度 = dr["TotalConsume"].ToString(),
                        优惠券额度 = dr["TotalCoupon"].ToString(),
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport{ Name = "资产概况", Icon = "Report1.icon", AutoReprotData = new AutoReprotData{ Type = ReportDataType.Amount ,  List = dataList }},
                new AutoReport{ Name = "数据走势图", Icon = "Report2.icon", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}