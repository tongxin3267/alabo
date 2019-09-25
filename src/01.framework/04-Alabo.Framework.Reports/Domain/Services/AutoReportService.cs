using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Reports.Domain.Dtos;
using Alabo.App.Core.Reports.Domain.Entities;
using Alabo.App.Core.Reports.Domain.Repositories;
using Alabo.App.Core.UI.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.Model;
using Alabo.Domains.Services;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;
using Alabo.Domains.Services.Report.Enums;
using Alabo.Extensions;
using Alabo.Linq.Dynamic;
using Alabo.Reflections;
using Alabo.UI;
using Alabo.UI.AutoReports;

namespace Alabo.App.Core.Reports.Domain.Services {

    ///
    public class AutoReportService : ServiceBase<Report, ObjectId>, IAutoReportService {
        private readonly IAutoReportRepository _autoReportRepository;

        /// <summary>
        /// AutoReportService
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="repository"></param>
        public AutoReportService(IUnitOfWork unitOfWork, IRepository<Report, ObjectId> repository)
            : base(unitOfWork, repository) {
            _autoReportRepository = Repository<IAutoReportRepository>();
        }

        /// <summary>
        /// 按天统计数量
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, List<AutoReport>> GetDayCountReport(CountReportInput dateCountReportInput) {
            List<AutoReport> returnList = new List<AutoReport>();
            string TempTable = string.Empty;
            //是否特殊字段处理
            int IsSpec = 0;

            #region 安全验证

            if (dateCountReportInput == null) {
                if (string.IsNullOrEmpty(dateCountReportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(dateCountReportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            #endregion 安全验证

            // 特殊字段处理
            var EnumName = string.Empty;
            if (dateCountReportInput.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(dateCountReportInput.Field);
                if (property != null) {
                    var filedType = property.GetType();
                    var filed = Reflection.GetPropertyValue(dateCountReportInput.Field, instanceFind);
                    if (filed.ToString() != "0") {
                        EnumName = property.PropertyType.Name;
                        IsSpec = 1;
                    }
                }
            }

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("表不存在"), returnList);
            }
            if (table.TableType == TableType.Mongodb) {
                var rs = Linq.Dynamic.DynamicService.ResolveMethod(typeFind.Name, "GetCountReport", dateCountReportInput);
                var rsList = rs.Item2 as List<AutoReport>;
            }

            if (table.TableType == TableType.SqlServer) {
                TempTable = table.TableName;
                // 调用Response的方法
                if (IsSpec == 1) {
                    returnList = GetReportFormWithField(dateCountReportInput, EnumName);
                } else {
                    dateCountReportInput.Condition.EntityType = dateCountReportInput.EntityType;
                    returnList = GetReportForm(dateCountReportInput);
                }
            }
            return Tuple.Create(ServiceResult.Success, returnList);
        }

        /// <summary>
        /// 按天统计数量
        /// </summary>
        /// <param name="inputParas"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, List<AutoReport>> GetCountReport2(CountReportInput inputParas) {
            var returnList = new List<AutoReport>();
            var TempTable = string.Empty;

            if (inputParas == null || inputParas.EntityType.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(inputParas.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            // 特殊字段处理
            if (inputParas.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(inputParas.Field);
                var filedType = property.GetType();
                var filed = Reflection.GetPropertyValue(inputParas.Field, instanceFind);
            }

            var rs = DynamicService.ResolveMethod(typeFind.Name, "GetCountReport", inputParas);
            var rsList = rs.Item2 as List<AutoReport>;
            return Tuple.Create(ServiceResult.Success, rsList);
        }

        /// <summary>
        /// 按天统计数量 扩展
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, List<AutoReport>> GetDayCountReportByField(CountReportInput dateCountReportInput) {
            List<AutoReport> returnList = new List<AutoReport>();
            string TempTable = string.Empty;

            #region 安全验证

            if (dateCountReportInput == null) {
                if (string.IsNullOrEmpty(dateCountReportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(dateCountReportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            #endregion 安全验证

            // 特殊字段处理
            if (dateCountReportInput.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(dateCountReportInput.Field);
                var filedType = property.GetType();
                if (filedType.Name != dateCountReportInput.Field) {
                    Tuple.Create(ServiceResult.FailedMessage("实体的字段不存在"), returnList);
                }
            }

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table.TableType == TableType.Mongodb) {
                // var serviceBase= DynamicService
                //var tlist = Resolve<ITableService>().GetList();
                //var result = from list in tlist
                //             group list by list.CreateTime into g
                //             select new { g.Key, Total = g.Sum(n => n.ToObjectId().ToInt64()) };
            }

            if (table.TableType == TableType.SqlServer) {
                TempTable = table.TableName;
            }
            //调用Response的方法

            //标记待更新
            var StartTime = DateTime.Now;
            var EndTime = DateTime.Now;

            returnList = GetReportForm(dateCountReportInput);
            return Tuple.Create(ServiceResult.Success, returnList);
        }

        #region 报表图状数据

        /// <summary>
        /// GetReportFormWithField:根据字段 日期 统计报表
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>
        protected List<AutoReport> GetReportFormWithField(CountReportInput dateCountReportInput, string EnumName) {
            var result = _autoReportRepository.GetReportFormWithField(dateCountReportInput, EnumName);
            return result;
        }

        /// <summary>
        /// 报表图状数据 扩展
        /// </summary>
        /// <returns></returns>

        protected List<AutoReport> GetReportFormByField(CountReportInput countReport) {
            List<AutoReport> returnList = new List<AutoReport>();
            returnList = _autoReportRepository.GetDayCountReportByFiled(countReport);
            return returnList;
        }

        /// <summary>
        /// 报表图状数据
        /// </summary>
        /// <returns></returns>
        protected List<AutoReport> GetReportForm(CountReportInput countReportInput) {
            List<object> returnList = new List<object>();
            returnList = _autoReportRepository.GetDayCountReport(countReportInput);
            List<object> tempObjectList = new List<object>();
            var chartCols = new List<string> { "日期", "数量", "比率" };

            foreach (var item in returnList) {
                var viewTableOutput = new {
                    日期 = item.GetType().GetProperty("Date").GetValue(item, null).ToString(),
                    数量 = item.GetType().GetProperty("Count").GetValue(item, null).ConvertToLong(),
                    比率 = item.GetType().GetProperty("Rate").GetValue(item, null).ToString(),
                };
                tempObjectList.Add(viewTableOutput);
            }

            var line = (ReportChartType)Enum.Parse(typeof(ReportChartType), "0");
            var Histogram = (ReportChartType)Enum.Parse(typeof(ReportChartType), "1");
            var returnReport = new List<AutoReport>
            {
                 new AutoReport{ Name = "", Icon =Flaticon.Alarm.GetIcon(), AutoReportChart = new AutoReportChart{ Type = line, Columns = chartCols, Rows = tempObjectList }},
            };

            return returnReport;
        }

        #endregion 报表图状数据

        /// <summary>
        /// 统计（Count）所有实体报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTableByField(CountReportInput reportInput) {
            PagedList<CountReportTable> returnList = new PagedList<CountReportTable>();

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            #endregion 安全验证

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table.TableType == TableType.Mongodb) {
                // var serviceBase= DynamicService
                //var tlist = Resolve<ITableService>().GetList();
                //var result = from list in tlist
                //             group list by list.CreateTime into g
                //             select new { g.Key, Total = g.Sum(n => n.ToObjectId().ToInt64()) };
            }

            //标记待更新
            var StartTime = DateTime.Now;
            var EndTime = DateTime.Now;

            if (table.TableType == TableType.SqlServer) {
                var tableName = table.TableName;

                returnList = GetTableForm(reportInput);
            }
            return Tuple.Create(ServiceResult.Success, returnList);
        }

        /// <summary>
        ///  根据指定字段-统计（count）所有实体报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTable(CountReportInput reportInput) {
            PagedList<CountReportTable> returnList = new PagedList<CountReportTable>();
            string TempTable = string.Empty;
            //是否特殊字段处理
            int IsSpec = 0;

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            #endregion 安全验证

            var EnumName = string.Empty;
            // 特殊字段处理
            if (reportInput.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(reportInput.Field);
                var filedType = property.GetType();
                var filed = Reflection.GetPropertyValue(reportInput.Field, instanceFind);
                if (filed.ToString() != "0") {
                    EnumName = property.PropertyType.Name;
                    IsSpec = 1;
                }
            }

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table.TableType == TableType.Mongodb) {
                // var serviceBase= DynamicService
                //var tlist = Resolve<ITableService>().GetList();
                //var result = from list in tlist
                //             group list by list.CreateTime into g
                //             select new { g.Key, Total = g.Sum(n => n.ToObjectId().ToInt64()) };
            }

            if (table.TableType == TableType.SqlServer) {
                TempTable = table.TableName;
                // 调用Response的方法
                if (IsSpec == 1) {
                    returnList = GetTableFormByField(reportInput, EnumName);
                } else {
                    returnList = GetTableForm(reportInput);
                }
            }
            return Tuple.Create(ServiceResult.Success, returnList);
        }

        /// <summary>
        ///  根据指定字段-统计（count）所有实体报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTable2(CountReportInput reportInput) {
            var returnList = new PagedList<CountReportTable>();
            string TempTable = string.Empty;

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            #endregion 安全验证

            // 特殊字段处理
            if (reportInput.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(reportInput.Field);
                var filedType = property.GetType();
                var filed = Reflection.GetPropertyValue(reportInput.Field, instanceFind);
            }

            var rs = DynamicService.ResolveMethod(typeFind.Name, "GetCountTable", reportInput);
            var rsList = rs.Item2 as PagedList<CountReportTable>;
            return Tuple.Create(ServiceResult.Success, rsList);
        }

        #region 报表表格数据

        /// <summary>
        /// 表格格式
        /// </summary>
        /// <returns></returns>

        protected PagedList<CountReportTable> GetTableForm(CountReportInput countReportInput) {
            List<object> objectList = new List<object>();
            //调用Response的方法
            objectList = _autoReportRepository.GetDayCountReport(countReportInput);
            var count = objectList.Count();
            IList<CountReportTable> resultList = new List<CountReportTable>();

            foreach (var item in objectList) {
                var viewTableOutput = new CountReportTable {
                    Date = item.GetType().GetProperty("Date").GetValue(item, null).ToString(),
                    Count = item.GetType().GetProperty("Count").GetValue(item, null).ConvertToLong(),
                    Rate = item.GetType().GetProperty("Rate").GetValue(item, null).ToString(),
                };
                resultList.Add(viewTableOutput);
            }

            var pgList = GetPagedListx(resultList, countReportInput.PageSize.ConvertToInt(), countReportInput.PageIndex);

            return pgList;
        }

        /// <summary>
        /// 特殊字段表格格式
        /// </summary>
        /// <param name="countReportInput"></param>
        /// <param name="EnumName"></param>
        /// <returns></returns>

        protected PagedList<CountReportTable> GetTableFormByField(CountReportInput countReportInput, string EnumName) {
            List<CountReportTable> CountTableOutputList = new List<CountReportTable>();
            CountTableOutputList = _autoReportRepository.GetCountReportTableWithField(countReportInput, EnumName);
            var result = PagedList<CountReportTable>.Create(CountTableOutputList, CountTableOutputList.Count(), countReportInput.PageIndex, countReportInput.PageSize);

            return result;
        }

        #endregion 报表表格数据

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="List"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        protected PagedList<CountTableOutput> GetPagedList(IList<CountTableOutput> List, int pageSize, int pageIndex) {
            if (pageSize < 1) {
                pageSize = 1;
            }

            if (pageIndex < 1) {
                pageIndex = 1;
            }

            long totalCount;

            IList<CountTableOutput> pgeResultList;
            if (true) {
                pgeResultList = List.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                totalCount = List.Count;
            }

            var pagedList = PagedList<CountTableOutput>.Create(pgeResultList, totalCount, pageSize, pageIndex);

            return pagedList;
        }

        protected PagedList<CountReportTable> GetPagedListx(IList<CountReportTable> List, int pageSize, int pageIndex) {
            if (pageSize < 1) {
                pageSize = 1;
            }

            if (pageIndex < 1) {
                pageIndex = 1;
            }

            long totalCount;

            IList<CountReportTable> pgeResultList;
            if (true) {
                pgeResultList = List.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                totalCount = List.Count;
            }

            var pagedList = PagedList<CountReportTable>.Create(pgeResultList, totalCount, pageSize, pageIndex);

            return pagedList;
        }

        /// <summary>
        /// 根据配置统计单一数字
        /// </summary>
        /// <param name="singleDataInpu"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, decimal> GetSingleData(SingleReportInput singleDataInpu) {
            decimal returnValue = 0m;

            #region 安全验证

            if (singleDataInpu == null) {
                if (string.IsNullOrEmpty(singleDataInpu.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnValue);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnValue);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(singleDataInpu.EntityType, ref typeFind, ref instanceFind);

            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnValue);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnValue);
            }

            #endregion 安全验证

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("表未找到"), returnValue);
            }
            if (table.TableType == TableType.Mongodb) {
                var rs = DynamicService.ResolveMethod(typeFind.Name, "GetSingleReportData", singleDataInpu);
                returnValue = rs.Item2.ConvertToDecimal();
            }

            if (table.TableType == TableType.SqlServer) {
                var tableName = table.TableName;

                var rs = _autoReportRepository.GetSingleData(singleDataInpu);
                return Tuple.Create(ServiceResult.Success, rs);
                // returnValue = _autoReportRepository.GetSingleData(tableName, singleDataInpu.Field, singleDataInpu.ReportType, singleDataInpu.StartTime, singleDataInpu.EndTime, singleDataInpu.SqlWhere);
            }
            return Tuple.Create(ServiceResult.Success, returnValue);
        }

        /// <summary>
        /// 根据字段求和 生成报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, PagedList<SumReportTable>> GetCountSumTable(SumReportInput reportInput) {
            PagedList<SumReportTable> resultPage = new PagedList<SumReportTable>();

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), resultPage);
                }
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), resultPage);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.EntityType, ref typeFind, ref instanceFind);

            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("所输入实体不存在"), resultPage);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), resultPage);
            }

            if (reportInput.Fields.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedMessage("统计的字段不能为空"), resultPage);
            }

            #endregion 安全验证

            var EnumName = string.Empty;
            // 特殊字段处理
            if (reportInput.Field.IsNotNullOrEmpty()) {
                var property = instanceFind.GetType().GetProperty(reportInput.Field);
                var filedType = property.GetType();
                var filed = Reflection.GetPropertyValue(reportInput.Field, instanceFind);
                if (filed.ToString() != "0") {
                    EnumName = property.PropertyType.Name;
                }
            }

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("查询的表不存在"), resultPage);
            }

            if (table.TableType == TableType.Mongodb) {
            }

            if (table.TableType == TableType.SqlServer) {
                var tableName = table.TableName;

                if (string.IsNullOrEmpty(tableName)) {
                    return Tuple.Create(ServiceResult.FailedMessage("查询的表不存在"), resultPage);
                }

                var resultList = _autoReportRepository.GetSumReportTable(reportInput, EnumName);
                resultPage = PagedList<SumReportTable>.Create(resultList, resultList.Count(), reportInput.PageIndex, reportInput.PageSize);

                return Tuple.Create(ServiceResult.Success, resultPage);
            }
            return Tuple.Create(ServiceResult.FailedMessage("查询失败"), resultPage);
        }

        /// <summary>
        /// 根据字段求和 生成报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, PagedList<SumReportTable>> GetCountSumTable2(SumTableInput reportInput) {
            PagedList<SumReportTable> resultPage = new PagedList<SumReportTable>();

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.Type)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), resultPage);
                }
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), resultPage);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.Type, ref typeFind, ref instanceFind);

            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("所输入实体不存在"), resultPage);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), resultPage);
            }

            if (reportInput.Fields.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedMessage("统计的字段不能为空"), resultPage);
            }

            #endregion 安全验证

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("查询的表不存在"), resultPage);
            }

            var rs = DynamicService.ResolveMethod(typeFind.Name, "GetSumReportTable", reportInput);
            var rsList = rs.Item2 as PagedList<SumReportTable>;
            return Tuple.Create(ServiceResult.Success, rsList);
        }

        /// <summary>
        /// 根据字段求和 统计报表
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, List<AutoReport>> GetSumReport(SumReportInput reportInput) {
            List<AutoReport> returnList = new List<AutoReport>();

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.EntityType)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.EntityType, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            if (reportInput.Fields == null) {
                return Tuple.Create(ServiceResult.FailedMessage("统计的字段不能为空"), returnList);
            }

            //验证传入的统计字段和表是否匹配
            var fieldsList = reportInput.Fields;

            #endregion 安全验证

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("查询的表不存在"), returnList);
            }

            if (table.TableType == TableType.Mongodb) {
            }

            if (table.TableType == TableType.SqlServer) {
                var tableName = table.TableName;

                if (tableName == null) {
                    return Tuple.Create(ServiceResult.FailedMessage("查询在表不存在"), returnList);
                }

                returnList = _autoReportRepository.GetSumReport(reportInput);
            }

            return Tuple.Create(ServiceResult.Success, returnList);
        }

        /// <summary>
        /// 根据字段求和 统计报表
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, List<AutoReport>> GetSumReport2(SumTableInput reportInput) {
            List<AutoReport> returnList = new List<AutoReport>();

            #region 安全验证

            if (reportInput == null) {
                if (string.IsNullOrEmpty(reportInput.Type)) {
                    return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
                }

                return Tuple.Create(ServiceResult.FailedMessage("实体类型不能为空"), returnList);
            }

            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(reportInput.Type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                return Tuple.Create(ServiceResult.FailedMessage("实体类型不存在"), returnList);
            }

            if (!(instanceFind is IEntity)) {
                // 非实体类型不能完成数据统计
                return Tuple.Create(ServiceResult.FailedMessage("非实体类型不能进行数据统计"), returnList);
            }

            if (reportInput.Fields.Count <= 0) {
                return Tuple.Create(ServiceResult.FailedMessage("统计的字段不能为空"), returnList);
            }

            //验证传入的统计字段和表是否匹配
            var fieldsList = reportInput.Fields;
            fieldsList.ForEach(p => {
            });

            #endregion 安全验证

            var table = Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);
            if (table == null) {
                return Tuple.Create(ServiceResult.FailedMessage("查询的表不存在"), returnList);
            }

            var rs = DynamicService.ResolveMethod(typeFind.Name, "GetSumReport", reportInput);
            var rsList = rs.Item2 as List<AutoReport>;
            return Tuple.Create(ServiceResult.Success, rsList);
        }
    }
}