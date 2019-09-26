using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Report;
using Alabo.Domains.Services.Report.Dtos;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Reports.Domain.Entities;
using Alabo.Framework.Reports.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Reports.Controllers {

    /// <summary>
    /// 报表
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Report/[action]")]
    public class ApiReportController : ApiBaseController<Report, ObjectId> {

        /// <summary>
        /// 报表 构造
        /// </summary>
        public ApiReportController() : base() {
            BaseService = Resolve<IReportService>();
        }

        #region 过期方法，仅供参考

        /// <summary>
        ///  按天统计数据-线型
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "按天统计数据")]
        public ApiResult<List<AutoReport>> GetCountReport2([FromQuery] CountReportInput reportInput) {
            var result = Resolve<IAutoReportService>().GetCountReport2(reportInput);
            return ToResult<List<AutoReport>>(result);
        }

        /// <summary>
        /// 按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "按天统计实体增加数据,输出表格形式")]
        public ApiResult<PagedList<CountReportTable>> GetCountTable2([FromQuery] CountReportInput reportInput) {
            var result = Resolve<IAutoReportService>().GetDayCountTable2(reportInput);
            return ToResult<PagedList<CountReportTable>>(result);
        }

        /// <summary>
        /// 根据字段状态,输出报表表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "根据字段状态,输出报表表格形式")]
        public ApiResult<PagedList<CountReportTable>> GetDayCountTableByField(CountReportInput reportInput) {
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(reportInput);
            return ToResult<PagedList<CountReportTable>>(result);
        }

        #region 求和 报表与表格 sql

        /// <summary>
        /// 按查询条件，求和统计
        /// </summary>
        [HttpGet]
        [Display(Description = "按查询条件，求和统计")]
        public ApiResult<List<AutoReport>> GetSumReport2([FromQuery] SumTableInput reportInput) {
            var result = Resolve<IAutoReportService>().GetSumReport2(reportInput);
            return ToResult(result);
        }

        /// <summary>
        /// 按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "按天统计实体增加数据,输出表格形式")]
        public ApiResult<PagedList<SumReportTable>> GetSumTable2([FromQuery] SumTableInput reportInput) {
            var result = Resolve<IAutoReportService>().GetCountSumTable2(null);
            return ToResult(result);
        }

        #endregion 求和 报表与表格 sql

        #endregion 过期方法，仅供参考

        #region 单条数据

        /// <summary>
        /// 根据配置统计单一数字 ok
        /// </summary>
        /// <param name="singleReportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "统计单一数字")]
        public ApiResult<decimal> GetSingleReport([FromBody] SingleReportInput singleReportInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<decimal>(this.FormInvalidReason());
            }
            var result = Resolve<IAutoReportService>().GetSingleData(singleReportInput);
            return ToResult<decimal>(result);
        }

        #endregion 单条数据

        #region 数量统计

        /// <summary>
        ///  按天统计数据-线型
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "按天统计数据")]
        public ApiResult<List<AutoReport>> GetCountReport([FromBody] CountReportInput reportInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<List<AutoReport>>(this.FormInvalidReason());
            }
            var result = Resolve<IAutoReportService>().GetDayCountReport(reportInput);
            return ToResult<List<AutoReport>>(result);
        }

        /// <summary>
        /// 按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "按天统计实体增加数据,输出表格形式")]
        public ApiResult<PagedList<CountReportTable>> GetCountTable([FromBody] CountReportInput reportInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PagedList<CountReportTable>>(this.FormInvalidReason());
            }
            var result = Resolve<IAutoReportService>().GetDayCountTable(reportInput);
            return ToResult<PagedList<CountReportTable>>(result);
        }

        #endregion 数量统计

        #region 求和统计

        /// <summary>
        /// 按查询条件，求和统计
        /// </summary>
        [HttpPost]
        [Display(Description = "按查询条件，求和统计")]
        public ApiResult<List<AutoReport>> GetSumReport([FromBody] SumReportInput reportInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<List<AutoReport>>(this.FormInvalidReason());
            }
            var result = Resolve<IAutoReportService>().GetSumReport(reportInput);
            return ToResult(result);
        }

        /// <summary>
        /// 按天统计实体增加数据,输出表格形式
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "按天统计实体增加数据,输出表格形式")]
        public ApiResult<PagedList<SumReportTable>> GetSumTable([FromBody] SumReportInput reportInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PagedList<SumReportTable>>(this.FormInvalidReason());
            }
            var result = Resolve<IAutoReportService>().GetCountSumTable(reportInput);
            return ToResult(result);
        }

        #endregion 求和统计
    }
}