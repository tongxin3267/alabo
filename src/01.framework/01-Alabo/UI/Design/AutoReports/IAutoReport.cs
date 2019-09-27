using System.Collections.Generic;

namespace Alabo.UI.Design.AutoReports
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