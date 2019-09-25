using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports.Domain.Entities;
using Alabo.Domains.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;
using Alabo.UI.AutoReports;

namespace Alabo.App.Core.Reports.Domain.Services {

    /// <summary>
    ///
    /// </summary>
    public interface IAutoReportService : IService<Report, ObjectId> {

        /// <summary>
        /// 按天统计数据
        /// 支持SqlService，和Mongdob数据库，可以统计出所有实体的增加数量
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, List<AutoReport>> GetDayCountReport(CountReportInput dateCountReportInput);

        /// <summary>
        /// 按天统计数据
        /// </summary>
        /// <param name="inputParas"></param>
        /// <returns></returns>
        Tuple<ServiceResult, List<AutoReport>> GetCountReport2(CountReportInput inputParas);

        /// <summary>
        /// 按天统计数量 扩展
        /// </summary>
        /// <param name="dateCountReportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, List<AutoReport>> GetDayCountReportByField(CountReportInput dateCountReportInput);

        /// <summary>
        /// 报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTable(CountReportInput reportInput);

        /// <summary>
        /// 报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTable2(CountReportInput reportInput);

        /// <summary>
        /// 报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, PagedList<CountReportTable>> GetDayCountTableByField(CountReportInput reportInput);

        Tuple<ServiceResult, List<AutoReport>> GetSumReport(SumReportInput reportInput);

        Tuple<ServiceResult, List<AutoReport>> GetSumReport2(SumTableInput reportInput);

        /// <summary>
        /// 根据字段求和 生成报表表格
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, PagedList<SumReportTable>> GetCountSumTable(SumReportInput reportInput);

        Tuple<ServiceResult, PagedList<SumReportTable>> GetCountSumTable2(SumTableInput reportInput);

        /// <summary>
        /// 根据配置统计单一数字
        /// </summary>
        /// <param name="singleDataInpu"></param>
        /// <returns></returns>
        Tuple<ServiceResult, decimal> GetSingleData(SingleReportInput singleDataInpu);
    }
}