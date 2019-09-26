using System.Collections.Generic;
using Alabo.Framework.Core.WebApis;
using Alabo.Domains.Services.Report;

namespace Alabo.Framework.Core.WebUis.Design.AutoReports
{
    public interface IAutoReport
    {
        /// <summary>
        ///     报表类型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        List<AutoReport> ResultList(object query, AutoBaseModel autoModel);
    }
}