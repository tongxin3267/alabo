﻿using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoReports.Enums;

namespace Alabo.App.Core.User.UI {

    public class UserReport : IAutoReport {

        /// <summary>
        /// AutoReport 结果返回
        /// </summary>
        /// <returns></returns>
        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel) {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            //总用户数
            var sqlMemberCount = $@" SELECT COUNT(*) FROM User_User ";
            var memberTotalCount = dbContext.ExecuteScalar(sqlMemberCount);
            //新用户数
            var sqlNewMember = $@"select count(*) from  [dbo].[User_User]
                                     where DateDiff(dd,CreateTime,getdate())<=3 ";
            var NewMemberTotal = dbContext.ExecuteScalar(sqlMemberCount);
            //近一个星期访客
            var sqlWeekMember = $@"select count(*) from  [dbo].[User_User]
                                     where DateDiff(dd,CreateTime,getdate())<=7";
            var WeekMemberCount = dbContext.ExecuteScalar(sqlWeekMember);

            //近一个月访客数
            var sqlMonthMember = $@"select count(*) from  [dbo].[User_User]
                                     where DateDiff(dd,CreateTime,getdate())<=31";
            var MonthMemberCount = dbContext.ExecuteScalar(sqlMonthMember);

            //总订单数
            var sqlTotalOrder = $@" SELECT COUNT(*) FROM ZKShop_Order";
            var TotalOrderCount = dbContext.ExecuteScalar(sqlTotalOrder);
            //新订单数
            var sqlNewOrder = $@" SELECT COUNT(*) FROM ZKShop_Order where DateDiff(dd,CreateTime,getdate())<=3";
            var newOrderCount = dbContext.ExecuteScalar(sqlNewOrder);
            //近一个星期订单
            var sqlWeekOrder = $@" SELECT COUNT(*) FROM ZKShop_Order WHERE DateDiff(dd,CreateTime,getdate())<=7";
            var WeekOrderCount = dbContext.ExecuteScalar(sqlNewOrder);
            //近一个月订单数
            var sqlMonthOrder = $@" SELECT COUNT(*) FROM ZKShop_Order WHERE DateDiff(dd,CreateTime,getdate())<=31 ";
            var MonthOrderCount = dbContext.ExecuteScalar(sqlNewOrder);

            //var outOrderRate = newOrderCount.ToString().ToDecimal() / 1.8m;

            var sqlCountByDay = $@" SELECT
                                    CONVERT(VARCHAR(100), a.CreateTime, 23) Day,
                                    COUNT(distinct a.id) as UserCount,
                                    COUNT(b.id) as UserOrderCount
                                    from [dbo].[User_User] as a inner join [dbo].[ZKShop_Order] as b on a.id=b.UserId
                                    GROUP BY CONVERT(VARCHAR(100), a.CreateTime, 23) ";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "总用户数",Intro=memberTotalCount.ToString(),Value = memberTotalCount.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "新用户数",Intro=NewMemberTotal.ToString(),Value = NewMemberTotal.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "近一个星期访客",Intro=WeekMemberCount.ToString(), Value = WeekMemberCount.ToString().ToDecimal(), Icon = "aa1.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "近一个月访客数",Intro=MonthMemberCount.ToString(), Value =MonthMemberCount.ToString().ToDecimal(), Icon = "aa2.ico"},

                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "总订单数",Intro=TotalOrderCount.ToString(),Value = TotalOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "新订单数", Intro=newOrderCount.ToString(),SubValue=5,Value = newOrderCount.ToString().ToDecimal(), Icon = "aa4.ico"},
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "近一个星期订单",Intro=WeekOrderCount.ToString(),SubValue=5, Value = WeekOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "近一个月订单数",Intro=MonthOrderCount.ToString(),SubValue=5, Value = MonthOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"},
            };

            var chartCols = new List<string> { "日期", "访问用户", "下单用户" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                while (dr.Read()) {
                    var item = new {
                        日期 = dr["Day"].ToString(),
                        访问用户 = dr["UserCount"].ToString(),
                        下单用户 = dr["UserOrderCount"].ToString(),
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport{ Name = "会员统计", Icon = Flaticon.AlertOff.GetIcon(), AutoReprotData = new AutoReprotData{ Type = ReportDataType.Amount ,  List = dataList }},
                new AutoReport{ Name = "数据走势图", Icon =  Flaticon.Alarm.GetIcon(), AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
                new AutoReport{ Name = "数据分布图", Icon =  Flaticon.Alarm.GetIcon(), AutoReportChart = new AutoReportChart{ Type = ReportChartType.Bar, Columns = chartCols, Rows = chartRows }},
                new AutoReport{ Name = "会员分布图", Icon =  Flaticon.Alarm.GetIcon(), AutoReportChart = new AutoReportChart{ Type = ReportChartType.Pie, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}