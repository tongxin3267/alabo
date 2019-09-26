﻿using System.Collections.Generic;
using Alabo.Core.WebApis;
using Alabo.Domains.Services.Report;

namespace Alabo.Core.WebUis.Design.AutoReports
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