﻿using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoReports;
using Alabo.UI.AutoReports.Enums;

namespace Alabo.App.Core.User.UI {

    public class PurchaseReport : IAutoReport {

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

            //总销售额
            var sqlSaleTotalAmount = $@" SELECT Sum([PaymentAmount]) FROM ZKShop_Order where [OrderStatus]=100 ";
            var SaleTotalAmount = dbContext.ExecuteScalar(sqlSaleTotalAmount);
            //总销售量
            var sqlSaleTotalCount = $@" SELECT Count(Id) FROM ZKShop_Order where [OrderStatus]=100";
            var SaleTotalCount = dbContext.ExecuteScalar(sqlSaleTotalCount);
            //当月销售额
            var sqlSaleMonthAmount = $@" SELECT Sum([PaymentAmount]) FROM ZKShop_Order where [OrderStatus]=100 and DateDiff(dd,CreateTime,getdate())<=31";
            var SaleMonthAmount = dbContext.ExecuteScalar(sqlSaleMonthAmount);
            //当月销售量
            var sqlMonthMemberCount = $@" SELECT COUNT(id) FROM ZKShop_Order  where [OrderStatus]=100 and DateDiff(dd,CreateTime,getdate())<=31";
            var NowMonthMemberCount = dbContext.ExecuteScalar(sqlMonthMemberCount);

            //var outOrderRate = newOrderCount.ToString().ToDecimal() / 1.8m;

            var sqlCountByDay = $@" SELECT
                                    CONVERT(VARCHAR(100), CreateTime, 23) Day,
                                    sum([PaymentAmount]) as OrderTotalAmount,
                                    count(1) as OrderTotalCount
                                    from ZKShop_Order
                                    where  OrderStatus=100
                                    group by  CONVERT(VARCHAR(100), CreateTime, 23)";

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

                new AutoReprotDataItem { Color = "Red", FontColor = "White", Name = "总销售额",Intro=SaleTotalAmount.ToString(),Value = SaleTotalAmount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Orange", FontColor = "White", Name = "总销售量", Intro=SaleTotalCount.ToString(),SubValue=5,Value = SaleTotalCount.ToString().ToDecimal(), Icon = "aa4.ico"},
                new AutoReprotDataItem { Color = "Green", FontColor = "White", Name = "当月销售额",Intro=SaleMonthAmount.ToString(),SubValue=5, Value = SaleMonthAmount.ToString().ToDecimal(), Icon = "aa3.ico"},
                new AutoReprotDataItem { Color = "Lime", FontColor = "White", Name = "当月销售量",Intro=NowMonthMemberCount.ToString(),SubValue=5, Value = NowMonthMemberCount.ToString().ToDecimal(), Icon = "aa3.ico"},
            };

            var chartCols = new List<string> { "日期", "访问用户", "下单用户" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                while (dr.Read()) {
                    var item = new {
                        日期 = dr["Day"].ToString(),
                        订单数量 = dr["OrderTotalAmount"].ToString(),
                        销售额 = dr["OrderTotalCount"].ToString(),
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport{ Name = "经营统计", Icon = "Report1.icon", AutoReprotData = new AutoReprotData{ Type = ReportDataType.Amount ,  List = dataList }},
                new AutoReport{ Name = "数据走势图", Icon = "Report2.icon", AutoReportChart = new AutoReportChart{ Type = ReportChartType.Line, Columns = chartCols, Rows = chartRows }},
            };
        }
    }
}