using System;
using System.Collections.Generic;
using Alabo.Core.WebApis;
using Alabo.Domains.Services.Report;

namespace Alabo.Core.WebUis.Design.AutoReports
{
    /// <summary>
    ///     实体统计报表
    /// </summary>
    public interface IEntityReport
    {
        /// <summary>
        ///     报表类型，实体统计报表
        /// </summary>
        /// <param name="entityType">实体类型，支持Mongodb和SqlService</param>
        /// <param name="autoModel">基础数据过滤对象,查询参数：比如时间，日期等查询条件</param>
        /// <returns></returns>
        List<AutoReport> ResultList(Type entityType, AutoBaseModel autoModel);
    }
}