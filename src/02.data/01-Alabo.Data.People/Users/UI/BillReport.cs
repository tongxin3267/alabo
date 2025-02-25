﻿using Alabo.Data.People.Users.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.Design.AutoReports;
using Alabo.UI.Design.AutoReports.Enums;
using System.Collections.Generic;

namespace Alabo.Data.People.Users.UI
{
    public class BillReport : IAutoReport
    {
        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel)
        {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            //总账单额度
            var sqlTotalBill = @" select sum(Amount) from [dbo].[Asset_Bill]   ";
            var totalBill = dbContext.ExecuteScalar(sqlTotalBill);
            //最近总账单额度
            var sqlNewTotalBill =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=3";
            var newTotalBill = dbContext.ExecuteScalar(sqlNewTotalBill);
            //最近七天总账单额度
            var sqlWeekTotalBill =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=7";
            var weekTotalBill = dbContext.ExecuteScalar(sqlWeekTotalBill);
            //最近一个月总账单额度
            var sqlMonthTotalBill =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=31";
            var monthTotalBill = dbContext.ExecuteScalar(sqlMonthTotalBill);

            //总充值金额
            var sqlTotalRecharge = @" select sum(Amount) from [dbo].[Asset_Bill]   ";
            var totalRecharge = dbContext.ExecuteScalar(sqlTotalRecharge);
            //总提现金额
            var sqlTotalCash =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=3";
            var totalCash = dbContext.ExecuteScalar(sqlTotalCash);
            //总消费金额
            var sqlTotalConsume =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=7";
            var totalConsume = dbContext.ExecuteScalar(sqlTotalConsume);
            //总销售金额
            var sqlTotalSale =
                @"select sum(Amount) from [dbo].[Asset_Bill] where  DateDiff(dd,CreateTime,getdate())<=31";
            var totalSale = dbContext.ExecuteScalar(sqlTotalSale);

            //多维度分析
            var sqlCountByDay = @" SELECT CONVERT(VARCHAR(100), CreateTime, 23) Day
                                    ,COUNT(CONVERT(VARCHAR(100), CreateTime, 23)) COUNT
                                    ,sum(Amount) as TotalAmout,
                                    sum([AfterAmount]) as TotalAfterAmount,
                                    SUM(CASE WHEN type = 201 THEN Amount ELSE 0 END) AS TotalConsume,
                                    SUM(CASE WHEN type = 110 THEN Amount ELSE 0 END) AS TotalSale
                                    from [dbo].[Asset_Bill]
                                    GROUP BY CONVERT(VARCHAR(100), CreateTime, 23) ";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem
                {
                    Color = "Green", FontColor = "White", Name = "总账单额度", Intro = totalBill.ToString(),
                    Value = totalBill.ToString().ToDecimal(), Icon = "aa1.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Lime", FontColor = "White", Name = "最近总账单额度", Intro = newTotalBill.ToString(),
                    Value = newTotalBill.ToString().ToDecimal(), Icon = "aa2.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Orange", FontColor = "White", Name = "最近七天总账单额度", Intro = weekTotalBill.ToString(),
                    Value = weekTotalBill.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Red", FontColor = "White", Name = "最近一个月总账单额度", Intro = monthTotalBill.ToString(),
                    Value = monthTotalBill.ToString().ToDecimal(), Icon = "aa4.ico"
                },

                new AutoReprotDataItem
                {
                    Color = "Green", FontColor = "White", Name = "总充值金额", Intro = totalRecharge.ToString(),
                    Value = totalRecharge.ToString().ToDecimal(), Icon = "aa1.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Lime", FontColor = "White", Name = "总提现金额", Intro = totalCash.ToString(),
                    Value = totalCash.ToString().ToDecimal(), Icon = "aa2.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Orange", FontColor = "White", Name = "总消费金额", Intro = totalConsume.ToString(),
                    Value = totalConsume.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Red", FontColor = "White", Name = "总销售金额", Intro = totalSale.ToString(),
                    Value = totalSale.ToString().ToDecimal(), Icon = "aa4.ico"
                }
            };
            var chartCols = new List<string> { "日期", "总账户额度", "帐单进出次数", "消费额度", "销售额度", "变动后账户额度" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay))
            {
                while (dr.Read())
                {
                    var item = new
                    {
                        日期 = dr["Day"].ToString(),
                        总账户额度 = dr["TotalAmout"].ToString(),
                        帐单进出次数 = dr["Count"].ToString(),
                        消费额度 = dr["TotalConsume"].ToString(),
                        销售额度 = dr["TotalSale"].ToString(),
                        变动后账户额度 = dr["TotalAfterAmount"].ToString()
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport
                {
                    Name = "账户概况", Icon = "Report1.icon",
                    AutoReprotData = new AutoReprotData {Type = ReportDataType.Amount, List = dataList}
                },
                new AutoReport
                {
                    Name = "数据走势图", Icon = "Report2.icon",
                    AutoReportChart = new AutoReportChart
                        {Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows}
                }
            };
        }
    }
}