using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Report;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Reports.Domain.Entities;
using Alabo.Framework.Reports.Domain.Services;
using Alabo.UI.Design.AutoReports;
using Alabo.UI.Design.AutoReports.Dtos;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Reports.Controllers
{
    /// <summary>
    ///     ����
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Report/[action]")]
    public class ApiReportController : ApiBaseController<Report, ObjectId>
    {
        /// <summary>
        ///     ���� ����
        /// </summary>
        public ApiReportController()
        {
            BaseService = Resolve<IReportService>();
        }

        #region ��������

        /// <summary>
        ///     ��������ͳ�Ƶ�һ���� ok
        /// </summary>
        /// <param name="singleReportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "ͳ�Ƶ�һ����")]
        public ApiResult<decimal> GetSingleReport([FromBody] SingleReportInput singleReportInput)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<decimal>(this.FormInvalidReason());
            }

            var result = Resolve<IAutoReportService>().GetSingleData(singleReportInput);
            return ToResult(result);
        }

        #endregion ��������

        #region ���ڷ����������ο�

        /// <summary>
        ///     ����ͳ������-����
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "����ͳ������")]
        public ApiResult<List<AutoReport>> GetCountReport2([FromQuery] CountReportInput reportInput)
        {
            var result = Resolve<IAutoReportService>().GetCountReport2(reportInput);
            return ToResult(result);
        }

        /// <summary>
        ///     ����ͳ��ʵ����������,��������ʽ
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "����ͳ��ʵ����������,��������ʽ")]
        public ApiResult<PagedList<CountReportTable>> GetCountTable2([FromQuery] CountReportInput reportInput)
        {
            var result = Resolve<IAutoReportService>().GetDayCountTable2(reportInput);
            return ToResult(result);
        }

        /// <summary>
        ///     �����ֶ�״̬,�����������ʽ
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "�����ֶ�״̬,�����������ʽ")]
        public ApiResult<PagedList<CountReportTable>> GetDayCountTableByField(CountReportInput reportInput)
        {
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(reportInput);
            return ToResult(result);
        }

        #region ��� �������� sql

        /// <summary>
        ///     ����ѯ���������ͳ��
        /// </summary>
        [HttpGet]
        [Display(Description = "����ѯ���������ͳ��")]
        public ApiResult<List<AutoReport>> GetSumReport2([FromQuery] SumTableInput reportInput)
        {
            var result = Resolve<IAutoReportService>().GetSumReport2(reportInput);
            return ToResult(result);
        }

        /// <summary>
        ///     ����ͳ��ʵ����������,��������ʽ
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "����ͳ��ʵ����������,��������ʽ")]
        public ApiResult<PagedList<SumReportTable>> GetSumTable2([FromQuery] SumTableInput reportInput)
        {
            var result = Resolve<IAutoReportService>().GetCountSumTable2(null);
            return ToResult(result);
        }

        #endregion ��� �������� sql

        #endregion ���ڷ����������ο�

        #region ����ͳ��

        /// <summary>
        ///     ����ͳ������-����
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����ͳ������")]
        public ApiResult<List<AutoReport>> GetCountReport([FromBody] CountReportInput reportInput)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<List<AutoReport>>(this.FormInvalidReason());
            }

            var result = Resolve<IAutoReportService>().GetDayCountReport(reportInput);
            return ToResult(result);
        }

        /// <summary>
        ///     ����ͳ��ʵ����������,��������ʽ
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����ͳ��ʵ����������,��������ʽ")]
        public ApiResult<PagedList<CountReportTable>> GetCountTable([FromBody] CountReportInput reportInput)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PagedList<CountReportTable>>(this.FormInvalidReason());
            }

            var result = Resolve<IAutoReportService>().GetDayCountTable(reportInput);
            return ToResult(result);
        }

        #endregion ����ͳ��

        #region ���ͳ��

        /// <summary>
        ///     ����ѯ���������ͳ��
        /// </summary>
        [HttpPost]
        [Display(Description = "����ѯ���������ͳ��")]
        public ApiResult<List<AutoReport>> GetSumReport([FromBody] SumReportInput reportInput)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<List<AutoReport>>(this.FormInvalidReason());
            }

            var result = Resolve<IAutoReportService>().GetSumReport(reportInput);
            return ToResult(result);
        }

        /// <summary>
        ///     ����ͳ��ʵ����������,��������ʽ
        /// </summary>
        /// <param name="reportInput"></param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "����ͳ��ʵ����������,��������ʽ")]
        public ApiResult<PagedList<SumReportTable>> GetSumTable([FromBody] SumReportInput reportInput)
        {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<PagedList<SumReportTable>>(this.FormInvalidReason());
            }

            var result = Resolve<IAutoReportService>().GetCountSumTable(reportInput);
            return ToResult(result);
        }

        #endregion ���ͳ��
    }
}