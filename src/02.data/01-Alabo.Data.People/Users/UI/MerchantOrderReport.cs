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
    public class MerchantOrderReport : IAutoReport
    {
        public List<AutoReport> ResultList(object query, AutoBaseModel autoModel)
        {
            var dbContext = Ioc.Resolve<IUserRepository>().RepositoryContext;

            //总订单数
            var sqlTotalOrder = @" SELECT COUNT(*) FROM Shop_Order";
            var totalOrderCount = dbContext.ExecuteScalar(sqlTotalOrder);
            //新订单数
            var sqlNewOrder = @" SELECT COUNT(*) FROM Shop_Order where DateDiff(dd,CreateTime,getdate())<=3";
            var newOrderCount = dbContext.ExecuteScalar(sqlNewOrder);
            //近一个星期订单
            var sqlWeekOrder = @" SELECT COUNT(*) FROM Shop_Order WHERE DateDiff(dd,CreateTime,getdate())<=7";
            var weekOrderCount = dbContext.ExecuteScalar(sqlNewOrder);
            //近一个月订单数
            var sqlMonthOrder = @" SELECT COUNT(*) FROM Shop_Order WHERE DateDiff(dd,CreateTime,getdate())<=31 ";
            var monthOrderCount = dbContext.ExecuteScalar(sqlNewOrder);

            //总销售额
            var sqlSaleTotalAmount = @" SELECT Sum([PaymentAmount]) FROM Shop_Order where [OrderStatus]=100 ";
            var saleTotalAmount = dbContext.ExecuteScalar(sqlSaleTotalAmount);
            //总销售量
            var sqlSaleTotalCount = @" SELECT Count(Id) FROM Shop_Order where [OrderStatus]=100";
            var saleTotalCount = dbContext.ExecuteScalar(sqlSaleTotalCount);
            //当月销售额
            var sqlSaleMonthAmount =
                @" SELECT Sum([PaymentAmount]) FROM Shop_Order where [OrderStatus]=100 and DateDiff(dd,CreateTime,getdate())<=31";
            var saleMonthAmount = dbContext.ExecuteScalar(sqlSaleMonthAmount);
            //当月销售量
            var sqlMonthMemberCount =
                @" SELECT COUNT(id) FROM Shop_Order  where [OrderStatus]=100 and DateDiff(dd,CreateTime,getdate())<=31";
            var monthMemberCount = dbContext.ExecuteScalar(sqlMonthMemberCount);

            var sqlCountByDay = @" SELECT
                                    CONVERT(VARCHAR(100), CreateTime, 23) Day,
                                    sum([PaymentAmount]) as OrderTotalAmount,
                                    count(1) as OrderTotalCount
                                    from Shop_Order
                                    where  OrderStatus=100
                                    group by  CONVERT(VARCHAR(100), CreateTime, 23)";

            var dataList = new List<AutoReprotDataItem>
            {
                new AutoReprotDataItem
                {
                    Color = "Red", FontColor = "White", Name = "总订单数", Intro = totalOrderCount.ToString(),
                    Value = totalOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Orange", FontColor = "White", Name = "新订单数", Intro = newOrderCount.ToString(),
                    SubValue = 5, Value = newOrderCount.ToString().ToDecimal(), Icon = "aa4.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Green", FontColor = "White", Name = "近一个星期订单", Intro = weekOrderCount.ToString(),
                    SubValue = 5, Value = weekOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Lime", FontColor = "White", Name = "近一个月订单数", Intro = monthOrderCount.ToString(),
                    SubValue = 5, Value = monthOrderCount.ToString().ToDecimal(), Icon = "aa3.ico"
                },

                new AutoReprotDataItem
                {
                    Color = "Red", FontColor = "White", Name = "总销售额", Intro = saleTotalAmount.ToString(),
                    Value = saleTotalAmount.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Orange", FontColor = "White", Name = "总销售量", Intro = saleTotalCount.ToString(),
                    SubValue = 5, Value = saleTotalCount.ToString().ToDecimal(), Icon = "aa4.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Green", FontColor = "White", Name = "当月销售额", Intro = saleMonthAmount.ToString(),
                    SubValue = 5, Value = saleMonthAmount.ToString().ToDecimal(), Icon = "aa3.ico"
                },
                new AutoReprotDataItem
                {
                    Color = "Lime", FontColor = "White", Name = "当月销售量", Intro = monthMemberCount.ToString(),
                    SubValue = 5, Value = monthMemberCount.ToString().ToDecimal(), Icon = "aa3.ico"
                }
            };
            var chartCols = new List<string> { "日期", "订单数量", "销售额" };

            var chartRows = new List<object>();
            using (var dr = dbContext.ExecuteDataReader(sqlCountByDay))
            {
                while (dr.Read())
                {
                    var item = new
                    {
                        日期 = dr["Day"].ToString(),
                        订单数量 = dr["OrderTotalAmount"].ToString(),
                        销售额 = dr["OrderTotalCount"].ToString()
                    };

                    chartRows.Add(item);
                }
            }

            return new List<AutoReport>
            {
                new AutoReport
                {
                    Name = "店商统计", Icon = "Report1.icon",
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