using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Framework.Reports.Domain.Entities;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.Design.AutoReports;
using Alabo.UI.Design.AutoReports.Dtos;
using Alabo.UI.Design.AutoReports.Enums;
using Alabo.Users.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Convert = System.Convert;

namespace Alabo.Framework.Reports.Domain.Repositories {

    /// <summary>
    ///     AutoReportRepository
    /// </summary>
    public class AutoReportRepository : RepositoryMongo<Report, ObjectId>, IAutoReportRepository {

        /// <summary>
        ///     AutoReportRepository
        /// </summary>
        /// <param name="unitOfWork"></param>
        public AutoReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     按天数统计数量 报图图状
        /// </summary>
        /// <returns></returns>
        public List<object> GetDayCountReport(CountReportInput countReportInput) {
            countReportInput.Condition.EntityType = countReportInput.EntityType;
            var tableName = countReportInput.Condition.GetTableName();
            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;

            var tempSqlWhere = string.Empty;

            //日期验证
            if (countReportInput.Condition.StartTime.ToString().IndexOf("0001") == -1 ||
                countReportInput.Condition.EndTime.ToString().IndexOf("0001") == -1) {
                var varStartTime = string.Format("{0:yyyy-MM-dd}", countReportInput.Condition.StartTime);
                var varEndTime = string.Format("{0:yyyy-MM-dd}", countReportInput.Condition.EndTime);
                tempSqlWhere = @" and (CONVERT(VARCHAR(100), CreateTime, 23) >='" + varStartTime +
                               "' and CONVERT(VARCHAR(100), CreateTime, 23)<= '" + varEndTime + "') ";
            }

            var sqlCountByDay = @" select  CONVERT(VARCHAR(100), CreateTime, 23) Day, count(" + countReportInput.Field +
                                @") as TotalNum,
                                cast( convert (decimal(18,2),100*cast(count(distinct isnull(" +
                                countReportInput.Field + @",0)) as float)/cast(((select  count(" +
                                countReportInput.Field + @") from " + tableName +
                                @") )as float) ) as varchar)+'%' as SaleRatio
                                from " + tableName +
                                @" where 1 = 1 " + tempSqlWhere +
                                @"group by CONVERT(VARCHAR(100), CreateTime, 23) ";

            var lists = new List<object>();
            if (FilterSqlScript(sqlCountByDay) != true) {
                using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                    while (dr.Read()) {
                        var item = ReadAutoReport(dr);
                        lists.Add(item);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        ///     按天数根据状态 统计数量 扩展 报图图状
        /// </summary>
        /// <returns></returns>
        public List<AutoReport> GetDayCountReportByFiled(CountReportInput countReport) {
            var tableName = "";
            var specialField = "";
            var startTime = DateTime.Now;
            var endTime = DateTime.Now;
            var sqlWhere = "";

            var tempSqlWhere = string.Empty;
            var chartCols = new List<string>();
            chartCols.Add("日期");
            chartCols.Add("全部数量");
            chartCols.Add("比率");

            //查询条件
            if (!string.IsNullOrEmpty(sqlWhere)) {
                tempSqlWhere = " and " + sqlWhere;
            }

            //日期验证
            if (startTime.ToString().IndexOf("0001") == -1 || endTime.ToString().IndexOf("0001") == -1) {
                var varStartTime = string.Format("{0:yyyy-MM-dd}", startTime);
                var varEndTime = string.Format("{0:yyyy-MM-dd}", endTime);
                tempSqlWhere = @" and (CONVERT(VARCHAR(100), CreateTime, 23) >='" + varStartTime +
                               "' and CONVERT(VARCHAR(100), CreateTime, 23)<= '" + varEndTime + "') ";
            }

            var tempSqlStatusCount = string.Empty;

            //枚举
            if (!string.IsNullOrEmpty(specialField)) {
                var typeEnum = specialField.GetTypeByName();

                foreach (Enum item in Enum.GetValues(typeEnum)) {
                    var itemName = item.GetDisplayName();
                    var itemValue = item.ToString();
                    var key = Convert.ToInt16(item);
                    tempSqlStatusCount = tempSqlStatusCount + @" count(CASE WHEN " + specialField + " =" + key +
                                         " THEN " + specialField + " END) AS  " + itemName + " , ";

                    chartCols.Add(itemName);
                }
            }

            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var sqlCountByDay = @" select  CONVERT(VARCHAR(100), CreateTime, 23) 日期, count(id) as 全部数量," +
                                tempSqlStatusCount +
                                @"cast( convert (decimal(18,2),100*cast(count(distinct isnull(id,0)) as float)/cast(((select  count(id) from " +
                                tableName + @") )as float) ) as varchar)+'%' as 比率
                                from " + tableName +
                                @" where 1 = 1 " + tempSqlWhere +
                                @" group by CONVERT(VARCHAR(100), CreateTime, 23) ";

            var result = new List<AutoReport>();

            var typeEnumExt = specialField.GetTypeByName();
            if (FilterSqlScript(sqlCountByDay) != true) {
                using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                    while (dr.Read()) {
                        var listItem = new List<AutoReportItem>();
                        var output = new AutoReport();

                        chartCols.ForEach(p => {
                            if (p.ToLower() == "日期") {
                                output.Date = dr[p].ToString();
                            } else if (p.ToLower() == "全部数量") {
                                output.Date = dr[p].ToString();
                            } else if (p.ToLower() == "比率") {
                                output.Date = dr[p].ToString();
                            } else {
                                listItem.Add(new AutoReportItem { Name = p.ToString(), Value = dr[p].ToString() });
                            }
                        });
                        output.AutoReportItem = listItem;
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     根据字段 日期 统计报表
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        public List<AutoReport> GetReportFormWithField(CountReportInput dateCountReportInput, string EnumName) {
            var queryResult = ExeclCountSql(dateCountReportInput, EnumName);
            var strSql = queryResult.Item1;
            var chartCols = queryResult.Item2;
            var chartRows = new List<object>();
            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;

            using (var dr = dbContext.ExecuteDataReader(strSql)) {
                while (dr.Read()) {
                    var dic = new Dictionary<string, string>();
                    chartCols.ForEach(p => { dic.Add(p, dr[p].ToString()); });
                    chartRows.Add(dic);
                }
            }

            var line = (ReportChartType)Enum.Parse(typeof(ReportChartType), "0");
            var result = new List<AutoReport>
            {
                new AutoReport
                {
                    Name = "数据走势图", Icon = Flaticon.Alarm.GetIcon(),
                    AutoReportChart = new AutoReportChart {Type = line, Columns = chartCols, Rows = chartRows}
                }
            };
            return result;
        }

        /// <summary>
        ///     根据日期 状态 统计报表表格
        /// </summary>
        /// <returns></returns>
        public List<object> GetDayCountReportTableByFiled(CountReportInput countReport) {
            var tableName = "";
            var specialField = "";
            var startTime = DateTime.Now;
            var endTime = DateTime.Now;
            var sqlWhere = "";
            //字段别名
            var otherColName = string.Empty;
            var tempSqlWhere = string.Empty;

            //查询条件
            if (!string.IsNullOrEmpty(sqlWhere)) {
                tempSqlWhere = " and " + sqlWhere;
            }
            //日期验证
            if (startTime.ToString().IndexOf("0001") == -1 || endTime.ToString().IndexOf("0001") == -1) {
                var varStartTime = string.Format("{0:yyyy-MM-dd}", startTime);
                var varEndTime = string.Format("{0:yyyy-MM-dd}", endTime);
                tempSqlWhere = @" and (CONVERT(VARCHAR(100), CreateTime, 23) >='" + varStartTime +
                               "' and CONVERT(VARCHAR(100), CreateTime, 23)<= '" + varEndTime + "') ";
            }

            var tempSqlStatusCount = string.Empty;
            var tempSqlStatusRatioCount = string.Empty;
            if (!string.IsNullOrEmpty(specialField)) {
                foreach (Status item in Enum.GetValues(typeof(Status))) {
                    var itemName = item.ToString();
                    var itemValue = (int)item;
                    //统计状态数量
                    tempSqlStatusCount = tempSqlStatusCount + @" count(CASE WHEN " + specialField + " =" + itemValue +
                                         " THEN " + specialField + " END) AS  CountStatus" + itemName + " , ";
                    //统计状态比率
                    tempSqlStatusRatioCount = tempSqlStatusRatioCount +
                                              @" cast( convert (decimal(18,2),100*cast(count(CASE WHEN " +
                                              specialField + "=" + itemValue +
                                              " THEN Status END) as float)/cast(((count(1) ) )as float) ) as varchar)+'%' as CountStatusRatio" +
                                              itemName + ",";
                }
            }

            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var sqlCountByDay = @" select  CONVERT(VARCHAR(100), CreateTime, 23) Day, count(id) as TotalNum," +
                                tempSqlStatusCount + tempSqlStatusRatioCount +
                                @"cast( convert (decimal(18,2),100*cast(count(distinct isnull(id,0)) as float)/cast(((select  count(id) from " +
                                tableName + @") )as float) ) as varchar)+'%' as SaleRatio
                                from " + tableName +
                                @" where 1 = 1 " + tempSqlWhere +
                                @" group by CONVERT(VARCHAR(100), CreateTime, 23) ";

            var lists = new List<object>();

            if (FilterSqlScript(sqlCountByDay) != true) {
                using (var dr = dbContext.ExecuteDataReader(sqlCountByDay)) {
                    while (dr.Read()) {
                        var item = ReadAutoReportTableSpec(dr);
                        lists.Add(item);
                    }
                }
            }

            return lists;
        }

        /// <summary>
        ///     根据日期 状态 统计报表表格
        /// </summary>
        /// <param name="countReport"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        public List<CountReportTable> GetCountReportTableWithField(CountReportInput countReport, string EnumName) {
            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var queryResult = ExeclCountTableSql(countReport, EnumName);
            var strSql = queryResult.Item1;
            var chartCols = queryResult.Item2;

            var chartRows = new List<object>();

            using (var dr = dbContext.ExecuteDataReader(strSql)) {
                while (dr.Read()) {
                    var dic = new Dictionary<string, string>();
                    foreach (var item in chartCols) {
                        var value = item.name;
                        var key = item.type;

                        if (!dic.ContainsKey(key)) {
                            dic.Add(key, dr[key].ToString());
                        }
                    }

                    chartRows.Add(dic);
                }
            }

            var result = new List<CountReportTable>
            {
                new CountReportTable
                    {Name = "报表数据", AutoReportChart = new CountReportItem {Columns = chartCols, Rows = chartRows}}
            };

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="singleReportInput"></param>
        /// <returns></returns>
        public decimal GetSingleData(SingleReportInput singleReportInput) {
            if (string.IsNullOrEmpty(singleReportInput.Field)) {
                singleReportInput.Field = "id";
            }

            singleReportInput.Condition.EntityType = singleReportInput.EntityType;
            var tableName = singleReportInput.Condition.GetTableName();

            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var sql = new StringBuilder();

            switch (singleReportInput.Style) {
                case ReportStyle.Count:
                    sql.Append(@"SELECT ISNULL(COUNT(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName);
                    break;

                case ReportStyle.Sum:
                    sql.Append(@"SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName);
                    break;

                case ReportStyle.Avg:
                    sql.Append(@"SELECT ISNULL(AVG(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName);
                    break;

                case ReportStyle.Min:
                    sql.Append(@"SELECT ISNULL(MIN(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName);
                    break;

                case ReportStyle.Max:
                    sql.Append(@"SELECT ISNULL(MAX(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName);
                    break;

                case ReportStyle.ChainRatioYesterday:
                    var yesterday = DateTime.Now.AddDays(-1);
                    var today = DateTime.Now;

                    sql.Append(@" SELECT ISNULL(T2." + singleReportInput.Field + " - T1." + singleReportInput.Field +
                               ",0) AS " + singleReportInput.Field + " FROM ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName +
                               " where convert(varchar(10),CreateTime,120)=convert(varchar(10),'" + yesterday +
                               "',120) )T1 left join ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName +
                               " where convert(varchar(10),CreateTime,120)=convert(varchar(10),'" + today +
                               "',120) )T2 on 1=1");
                    break;

                case ReportStyle.ChainRatioLastWeek:
                    var day = (int)DateTime.Now.DayOfWeek;

                    var thisWeekStart = DateTime.Now.AddDays(-day + 1).ToString("yyyy-MM-dd 00:00:00"); //本周第一天
                    var thisWeekNow = DateTime.Now; //当前天

                    var lastWeekStart = DateTime.Now.AddDays(-day - 6).ToString("yyyy-MM-dd 00:00:00"); //上周第一天
                    var lastWeekEnd =
                        DateTime.Now.AddDays(-day - 6).AddDays(day - 1).ToString("yyyy-MM-dd 23:59:59"); //上周对应当天

                    sql.Append(@" SELECT ISNULL(T2." + singleReportInput.Field + "-T1." + singleReportInput.Field +
                               ",0) AS " + singleReportInput.Field + " FROM ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName + " where CreateTime between '" +
                               lastWeekStart + "' and '" + lastWeekEnd + "' )T1 left join ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName + " where CreateTime between '" +
                               thisWeekStart + "' and '" + thisWeekNow + "'  )T2 on 1=1");
                    break;

                case ReportStyle.ChainRatioLastMonth:
                    var thisMonthStart = DateTime.Now.GetMonthBegin().ToString("yyyy-MM-dd 00:00:00"); //本月第一天
                    var thisMonthNow = DateTime.Now; //当前天

                    var lastMonthStart = DateTime.Now.GetMonthBegin().AddMonths(-1); //上个月第一天
                    var lastMonthEnd = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd 23:59:59"); //上月对应当天

                    sql.Append(@" SELECT ISNULL(T2." + singleReportInput.Field + "-T1." + singleReportInput.Field +
                               ",0) AS " + singleReportInput.Field + " FROM ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName + " where CreateTime between '" +
                               lastMonthStart + "' and '" + lastMonthEnd + "' )T1 left join ");
                    sql.Append(@" (SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " +
                               singleReportInput.Field + " FROM " + tableName + " where CreateTime between '" +
                               thisMonthStart + "' and '" + thisMonthNow + "'  )T2 on 1=1");
                    break;

                case ReportStyle.ChainRatioLastQuarter:

                    sql.Append(@" SELECT ISNULL(T2." + singleReportInput.Field + "- T1." + singleReportInput.Field +
                               ",0) AS " + singleReportInput.Field + " FROM ");
                    sql.Append(@"(select ISNULL(SUM(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName +
                               " where  DATEPART(qq,CreateTime)= DATEPART(qq,GETDATE())-1)T1 left join ");
                    sql.Append(@"(select ISNULL(SUM(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName +
                               " where  DATEPART(qq,CreateTime)= DATEPART(qq,GETDATE()))T2 on 1=1 ");
                    break;

                case ReportStyle.ChainRatioLastYear:
                    sql.Append(@"SELECT ISNULL(SUM(" + singleReportInput.Field + "),0) AS " + singleReportInput.Field +
                               " FROM " + tableName);
                    break;
            }

            sql.Append(singleReportInput.Condition.ToSqlWhere(singleReportInput.Style));
            var returnValue = dbContext.ExecuteScalar(sql.ToString());
            if (returnValue == null) {
                returnValue = 0;
            }

            return returnValue.ToDecimal();
        }

        /// <summary>
        ///     ExeclCountSql：根据特殊字段 统计报表图状语句
        /// </summary>
        /// <param name="countReportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        private Tuple<string, List<string>> ExeclCountSql(CountReportInput countReportInput, string EnumName) {
            var tempSqlWhere = string.Empty;
            var chartCols = new List<string>();
            chartCols.Add("日期");
            chartCols.Add("全部数量");
            chartCols.Add("比率");

            #region 条件查询

            ////查询条件
            //if (!string.IsNullOrEmpty(sqlWhere))
            //{
            //    tempSqlWhere = " and " + sqlWhere;
            //}
            // tempSqlWhere += countReportInput.Condition.ToSqlWhere();

            //日期验证
            if (countReportInput.Condition.StartTime.ToString().IndexOf("0001") == -1 ||
                countReportInput.Condition.EndTime.ToString().IndexOf("0001") == -1) {
                var varStartTime = string.Format("{0:yyyy-MM-dd}", countReportInput.Condition.StartTime);
                var varEndTime = string.Format("{0:yyyy-MM-dd}", countReportInput.Condition.EndTime);
                tempSqlWhere = @" and (CONVERT(VARCHAR(100), CreateTime, 23) >='" + varStartTime +
                               "' and CONVERT(VARCHAR(100), CreateTime, 23)<= '" + varEndTime + "') ";
            }

            #endregion 条件查询

            var tempSqlStatusCount = string.Empty;

            countReportInput.Condition.EntityType = countReportInput.EntityType;
            var tableName = countReportInput.Condition.GetTableName();

            //枚举
            if (!string.IsNullOrEmpty(EnumName)) {
                var typeEnum = EnumName.GetTypeByName();
                if (typeEnum != null) {
                    foreach (Enum item in Enum.GetValues(typeEnum)) {
                        var itemName = item.GetDisplayName();
                        itemName = FilterSpecial(itemName);
                        var itemValue = item.ToString();
                        var key = Convert.ToInt16(item);
                        tempSqlStatusCount = tempSqlStatusCount + @" count(CASE WHEN " + countReportInput.Field + " =" +
                                             key + " THEN " + countReportInput.Field + " END) AS  " + itemName + " , ";
                        chartCols.Add(itemName);
                    }
                }
            }

            var sqlCountByDay = @" select  CONVERT(VARCHAR(100), CreateTime, 23) 日期, count(id) as 全部数量," +
                                tempSqlStatusCount +
                                @"cast( convert (decimal(18,2),100*cast(count(distinct isnull(id,0)) as float)/cast(((select  count(id) from " +
                                tableName + @") )as float) ) as varchar)+'%' as 比率
                                from " + tableName +
                                @" where 1 = 1 " + tempSqlWhere +
                                @" group by CONVERT(VARCHAR(100), CreateTime, 23) ";

            return Tuple.Create(sqlCountByDay, chartCols);
        }

        /// <summary>
        ///     ExeclCountTableSql 根据特殊字段统计报表表格语句
        /// </summary>
        /// <param name="reportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        private Tuple<string, List<Columns>> ExeclCountTableSql(CountReportInput reportInput, string EnumName) {
            var listCol = new List<Columns>();
            var col = new Columns();
            var tempSqlWhere = string.Empty;

            col.name = "日期";
            col.type = "date";
            listCol.Add(col);
            col = new Columns();
            col.name = "全部数量";
            col.type = "count";
            listCol.Add(col);
            col = new Columns();
            col.name = "比率";
            col.type = "rate";
            listCol.Add(col);

            #region 条件查询

            ////查询条件
            //if (!string.IsNullOrEmpty(sqlWhere))
            //{
            //    tempSqlWhere = " and " + sqlWhere;
            //}

            //日期验证
            if (reportInput.Condition.StartTime.ToString().IndexOf("0001") == -1 ||
                reportInput.Condition.EndTime.ToString().IndexOf("0001") == -1) {
                var varStartTime = string.Format("{0:yyyy-MM-dd}", reportInput.Condition.StartTime);
                var varEndTime = string.Format("{0:yyyy-MM-dd}", reportInput.Condition.EndTime);
                tempSqlWhere = @" and (CONVERT(VARCHAR(100), CreateTime, 23) >='" + varStartTime +
                               "' and CONVERT(VARCHAR(100), CreateTime, 23)<= '" + varEndTime + "') ";
            }

            #endregion 条件查询

            var tempSqlStatusCount = string.Empty;
            var tempSqlStatusRatioCount = string.Empty;

            reportInput.Condition.EntityType = reportInput.EntityType;
            var tableName = reportInput.Condition.GetTableName();

            //枚举
            if (!string.IsNullOrEmpty(EnumName)) {
                var typeEnum = EnumName.GetTypeByName();

                foreach (Enum item in Enum.GetValues(typeEnum)) {
                    var itemName = item.GetDisplayName();
                    itemName = FilterSpecial(itemName);
                    var itemValue = item.ToString();
                    var key = Convert.ToInt16(item);
                    col = new Columns();
                    //统计状态数量
                    tempSqlStatusCount = tempSqlStatusCount + @" count(CASE WHEN " + reportInput.Field + " =" + key +
                                         " THEN " + reportInput.Field + " END) AS  " + itemValue + " , ";
                    col.name = itemName;
                    col.type = itemValue;
                    listCol.Add(col);

                    col = new Columns();
                    //统计状态比率字段
                    var itemNameRatio = itemName + "比率";
                    var itemNameRatioValue = itemValue + "Rate";
                    col.name = itemNameRatio;
                    col.type = itemNameRatioValue;

                    //统计状态比率
                    tempSqlStatusRatioCount = tempSqlStatusRatioCount +
                                              @" cast( convert (decimal(18,2),100*cast(count(CASE WHEN " +
                                              reportInput.Field + "=" + key + " THEN " + reportInput.Field +
                                              " END) as float)/cast(((count(1) ) )as float) ) as varchar)+'%' as " +
                                              itemNameRatioValue + " , ";

                    listCol.Add(col);
                }
            }

            var sqlCountByDay = @" select  CONVERT(VARCHAR(100), CreateTime, 23) date, count(1) as count," +
                                tempSqlStatusCount + tempSqlStatusRatioCount +
                                @"cast( convert (decimal(18,2),100*cast(count(distinct isnull(id,0)) as float)/cast(((select  count(id) from " +
                                tableName + @") )as float) ) as varchar)+'%' as rate
                                from " + tableName +
                                @" where 1 = 1 " + tempSqlWhere +
                                @" group by CONVERT(VARCHAR(100), CreateTime, 23) ";

            return Tuple.Create(sqlCountByDay, listCol);
        }

        /// <summary>
        ///     图形报表普通实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private object ReadAutoReport(IDataReader reader) {
            var item = new {
                Date = reader["Day"].ToString(),
                Count = reader["TotalNum"].ToString(),
                Rate = reader["SaleRatio"].ToString()
            };
            return item;
        }

        /// <summary>
        ///     图形报表特殊实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private object ReadAutoReportSpec(IDataReader reader) {
            var item = new {
                Date = reader["Day"].ToString(),
                Count = reader["TotalNum"].ToString(),
                Rate = reader["SaleRatio"].ToString()
            };
            return item;
        }

        private object ReadAutoReportSpec1(IDataReader reader, string specialField) {
            var typeEnum = specialField.GetTypeByName();
            object obj = null;

            foreach (Enum il in Enum.GetValues(typeEnum)) {
                var itemValue = il.ToString();
                itemValue = reader[itemValue].ToString();
                obj = new {
                    ItemValue = reader[itemValue].ToString()
                };
            }

            return obj;
        }

        /// <summary>
        ///     图形报表表格特殊实体
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private object ReadAutoReportTableSpec(IDataReader reader) {
            var item = new {
                Date = reader["Day"].ToString(),
                Count = reader["TotalNum"].ToString(),

                CountStatusNormal = reader["CountStatusNormal"].ToString(),
                CountStatusRatioNormal = reader["CountStatusRatioNormal"].ToString(),

                CountStatusFreeze = reader["CountStatusFreeze"].ToString(),
                CountStatusRatioFreeze = reader["CountStatusRatioFreeze"].ToString(),

                CountStatusDeleted = reader["CountStatusDeleted"].ToString(),
                CountStatusRatioDeleted = reader["CountStatusRatioDeleted"].ToString(),

                SaleRatio = reader["SaleRatio"].ToString()
            };
            return item;
        }

        #region 过滤工具

        /// <summary>
        ///     过滤Sql脚本关键字
        /// </summary>
        /// <param name="sSql">The s SQL.</param>
        private bool FilterSqlScript(string sSql) {
            int srcLen, decLen = 0;
            sSql = sSql.ToLower().Trim();
            srcLen = sSql.Length;
            sSql = sSql.Replace("exec", "");
            sSql = sSql.Replace("delete", "");
            sSql = sSql.Replace("master", "");
            sSql = sSql.Replace("truncate", "");
            sSql = sSql.Replace("declare", "");
            sSql = sSql.Replace("create", "");
            sSql = sSql.Replace("xp_", "no");
            decLen = sSql.Length;
            if (srcLen == decLen) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     过滤特殊字符
        ///     如果字符串为空，直接返回。
        /// </summary>
        /// <param name="str">需要过滤的字符串</param>
        /// <returns>过滤好的字符串</returns>
        public static string FilterSpecial(string str) {
            if (str == "") {
                return str;
            }

            str = str.Replace("'", "");
            str = str.Replace("<", "");
            str = str.Replace(">", "");
            str = str.Replace("%", "");
            str = str.Replace("'delete", "");
            str = str.Replace("''", "");
            str = str.Replace("\"\"", "");
            str = str.Replace(",", "");
            str = str.Replace(".", "");
            str = str.Replace(">=", "");
            str = str.Replace("=<", "");
            str = str.Replace("-", "");
            str = str.Replace("_", "");
            str = str.Replace(";", "");
            str = str.Replace("||", "");
            str = str.Replace("[", "");
            str = str.Replace("]", "");
            str = str.Replace("&", "");
            str = str.Replace("#", "");
            str = str.Replace("/", "");
            str = str.Replace("-", "");
            str = str.Replace("|", "");
            str = str.Replace("?", "");
            str = str.Replace(">?", "");
            str = str.Replace("?<", "");
            str = str.Replace(" ", "");
            return str;
        }

        #endregion 过滤工具

        #region 求和统计 报表-表格

        /// <summary>
        ///     根据字段求和 统计报表
        /// </summary>
        /// <returns></returns>
        public List<AutoReport> GetSumReport(SumReportInput sumReportInput) {
            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var queryResult = GetSumReportQuerySql(sumReportInput);
            var strSql = queryResult.Item1;
            var chartCols = queryResult.Item2;
            var chartRows = new List<object>();

            using (var dr = dbContext.ExecuteDataReader(strSql)) {
                while (dr.Read()) {
                    var dic = new Dictionary<string, string>();
                    chartCols.ForEach(p => { dic.Add(p, dr[p].ToString()); });
                    chartRows.Add(dic);
                }
            }

            var line = (ReportChartType)Enum.Parse(typeof(ReportChartType), "0");
            var result = new List<AutoReport>
            {
                new AutoReport
                {
                    Name = "数据走势图", Icon = Flaticon.Alarm.GetIcon(),
                    AutoReportChart = new AutoReportChart {Type = line, Columns = chartCols, Rows = chartRows}
                }
            };
            return result;
        }

        /// <summary>
        ///     按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        public List<SumReportTable> GetSumReportTable(SumReportInput reportInput, string EnumName) {
            var dbContext = Ioc.Resolve<IAlaboUserRepository>().RepositoryContext;
            var queryResult = GetSumTableQuerySql(reportInput, EnumName);

            var strSql = queryResult.Item1;
            var chartCols = queryResult.Item2;

            var chartRows = new List<object>();

            using (var dr = dbContext.ExecuteDataReader(strSql)) {
                while (dr.Read()) {
                    var dic = new Dictionary<string, string>();

                    foreach (var item in chartCols) {
                        var value = item.name;
                        var key = item.type;

                        if (!dic.ContainsKey(key)) {
                            dic.Add(key, dr[key].ToString());
                        }
                    }

                    chartRows.Add(dic);
                }
            }

            var result = new List<SumReportTable>
            {
                new SumReportTable
                {
                    Name = "报表数据", SumReportTableItem = new SumReportTableItems {Columns = chartCols, Rows = chartRows}
                }
            };

            return result;
        }

        /// <summary>
        ///     获取Sum报表Sql语句
        /// </summary>
        /// <param name="sumReportInput"></param>
        /// <returns></returns>
        private Tuple<string, List<string>> GetSumReportQuerySql(SumReportInput sumReportInput) {
            var tempSqlField = string.Empty;
            var chartCols = new List<string>();
            chartCols.Add("日期");

            //实体字段 过滤
            foreach (var field in sumReportInput.Fields) {
                var tempDisplayName = sumReportInput.EntityType.GetFiledDisplayName(field);

                tempSqlField = tempSqlField + " ,sum(" + field + ") as " + tempDisplayName;
                chartCols.Add(tempDisplayName); //根据传入的查询字段个数
            }
            ////查询条件
            //if (!string.IsNullOrEmpty(sqlWhere))
            //{
            //    tempSqlWhere = tempSqlWhere + " and " + sqlWhere;
            //}

            //日期验证
            var tempSqlWhere = string.Empty;

            if (sumReportInput.Condition.StartTime.ToString().IndexOf("0001") == -1 ||
                sumReportInput.Condition.EndTime.ToString().IndexOf("0001") == -1) {
                tempSqlWhere = " and  CreateTime between CONVERT(VARCHAR(100), '" + sumReportInput.Condition.StartTime +
                               "', 23) and CONVERT(VARCHAR(100), '" + sumReportInput.Condition.EndTime + "', 23)";
            }

            sumReportInput.Condition.EntityType = sumReportInput.EntityType;

            var tableName = sumReportInput.Condition.GetTableName();
            var sqlBaseExec = @" select CONVERT(VARCHAR(100), CreateTime, 23) as 日期
                                   " + tempSqlField + @"
                                    from " + tableName + @"
                                    where 1=1 " + tempSqlWhere +
                              @"group by CONVERT(VARCHAR(100), CreateTime, 23)";
            if (FilterSqlScript(sqlBaseExec)) {
                throw new ArgumentNullException("sql语句有异常！");
            }

            return Tuple.Create(sqlBaseExec, chartCols);
        }

        /// <summary>
        ///     sum报表表格Sql语句
        /// </summary>
        /// <param name="reportInput">表字段</param>
        /// <returns></returns>
        private Tuple<string, List<SumColumns>> GetSumTableQuerySql(SumReportInput reportInput, string EnumName) {
            //还得扩展 SpecialField
            var tempSqlWhere = string.Empty;
            var tempSqlField = string.Empty;
            var tempSqlStatusCount = string.Empty;
            var colList = new List<SumColumns>();
            var col = new SumColumns();
            col.name = "日期";
            col.type = "Date";
            colList.Add(col);

            if (!string.IsNullOrEmpty(EnumName)) {
                var typeEnum = EnumName.GetTypeByName();
                if (typeEnum != null) {
                    foreach (Enum item in Enum.GetValues(typeEnum)) {
                        var itemName = item.GetDisplayName();
                        itemName = FilterSpecial(itemName);
                        var itemValue = item.ToString();
                        var itemKey = Convert.ToInt16(item);
                        col = new SumColumns();
                        //实体字段
                        foreach (var field in reportInput.Fields) {
                            var tempDisplayName = reportInput.EntityType.GetFiledDisplayName(field);
                            //统计 Sum状态数量
                            var TotalAmountName = tempDisplayName + itemName;
                            var TotalAmountValue = field + itemValue;
                            //待完成 已完成 等状态
                            tempSqlStatusCount = tempSqlStatusCount + @" , SUM(CASE WHEN " + reportInput.Field + "=" +
                                                 itemKey + " THEN " + field + " ELSE 0 END) AS " + TotalAmountValue;
                            //var tempTableHeadName= tempDisplayName+"["+ itemName + "]";
                            col.name = TotalAmountName;
                            col.type = TotalAmountValue;
                            colList.Add(col);
                        }
                    }
                }
            }

            if (reportInput.Fields.Count > 0) //实体字段 过滤
{
                foreach (var field in reportInput.Fields) {
                    var tempDisplayName = reportInput.EntityType.GetFiledDisplayName(field);
                    tempSqlField = tempSqlField + " ,sum(" + field + ") as " + field;

                    col = new SumColumns();
                    col.name = tempDisplayName;
                    col.type = field;
                    colList.Add(col);
                }
            }
            ////查询条件
            //if (!string.IsNullOrEmpty(sqlWhere))
            //{
            //    tempSqlWhere = tempSqlWhere + " and " + sqlWhere;
            //}

            //日期验证
            if (reportInput.Condition.StartTime.ToString().IndexOf("0001") == -1 ||
                reportInput.Condition.EndTime.ToString().IndexOf("0001") == -1) {
                tempSqlWhere = tempSqlWhere + "  and  CreateTime between CONVERT(VARCHAR(100), '" +
                               reportInput.Condition.StartTime + "', 23) and CONVERT(VARCHAR(100), '" +
                               reportInput.Condition.EndTime + "', 23)";
            }

            reportInput.Condition.EntityType = reportInput.EntityType;
            var tableName = reportInput.Condition.GetTableName();
            var sqlBaseExec = @" select CONVERT(VARCHAR(100), CreateTime, 23) as Date
                                   " + tempSqlField + tempSqlStatusCount + @"
                                    from " + tableName + @"
                                    where 1=1 " + tempSqlWhere + @"
                                    group by CONVERT(VARCHAR(100), CreateTime, 23)";
            if (FilterSqlScript(sqlBaseExec)) {
                throw new ArgumentNullException("sql语句有异常！");
            }

            return Tuple.Create(sqlBaseExec, colList);
        }

        #endregion 求和统计 报表-表格
    }
}